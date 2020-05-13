using nQuant;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;

namespace ThinkGeo.MapSuite
{
    public class SQLiteBitmapTileCache : BitmapTileCache
    {
        private string connectionString;

        private const string defaultCacheId = "cacheImagesTable";
        private const string defaultCacheFile = "tileCache.db";

        private const string keyColumnName = "imageKey";
        private const string blobColumnName = "imageBlob";

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public static string DefaultCacheFile => defaultCacheFile;
        public static string DefaultCacheId => defaultCacheId;

        public SQLiteBitmapTileCache()
        : this(defaultCacheFile)
        { }

        public SQLiteBitmapTileCache(string cacheFile)
            : this(cacheFile, defaultCacheId)
        { }

        public SQLiteBitmapTileCache(string cacheFile, string cacheId)
            : this(cacheFile, cacheId, TileImageFormat.Png, new MapSuiteTileMatrix(590591790))
        { }

        public SQLiteBitmapTileCache(string cacheFile, string cacheId, TileImageFormat imageFormat, TileMatrix tileMatrix)
            : base(cacheId, imageFormat, tileMatrix)
        {
            string sqliteStatement = $"CREATE TABLE IF NOT EXISTS {defaultCacheId} ({keyColumnName} TEXT, {blobColumnName} BLOB)";
            ExecuteSqlNonQuery(sqliteStatement);
        }

        protected override BitmapTile GetTileCore(long row, long column)
        {
            TileMatrixCell cell = this.TileMatrix.GetCell(row, column);
            BitmapTile tile = new BitmapTile(cell.BoundingBox, this.TileMatrix.Scale);

            try
            {
                string Key = string.Format("{0}/{1}/{2}-{3}" + ImageFormat.ToString(), CacheId.ToLowerInvariant(), this.TileMatrix.Scale, row, column);

                string sqliteStatement = $"select * from  {defaultCacheId} where {keyColumnName} = '{Key}'";

                Collection<GeoImage> images = GetImagesByColumnName(sqliteStatement, blobColumnName);
                if (images.Count > 0)
                {
                    tile.Bitmap = images[0];
                }

            }
            catch (Exception)
            {
            }

            return tile;
        }

        protected override void SaveTileCore(Tile tile)
        {
            MemoryStream bitmapStream = null;

            try
            {
                BitmapTile bitmapTile = (BitmapTile)tile;
                this.TileMatrix.Scale = tile.Scale;
                TileMatrixCell cell = this.TileMatrix.GetCell(tile.BoundingBox.GetCenterPoint());

                string Key = string.Format(@"{0}/{1}/{2}-{3}" + ImageFormat.ToString(), CacheId.ToLowerInvariant(), bitmapTile.Scale, cell.Row, cell.Column);

                bitmapStream = GetStreamFromBitmap((Bitmap)bitmapTile.Bitmap.NativeImage);

                byte[] buffer = bitmapStream.ToArray();
                bitmapStream.Close();

                string sqliteStatement = $"INSERT OR REPLACE INTO {defaultCacheId} ({keyColumnName}, {blobColumnName}) values (@imageKey, @imageBlob)";
                SQLiteParameter[] parameters = {
                        new SQLiteParameter("@imageKey", DbType.String, 100),
                        new SQLiteParameter("@imageBlob", DbType.Binary)
                };

                parameters[0].Value = Key;
                parameters[1].Value = buffer;
                ExecuteSqlNonQuery(sqliteStatement, parameters);
            }
            finally
            {
                if (bitmapStream != null) { bitmapStream.Dispose(); }
            }
        }

        protected override void ClearCacheCore()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteTileCore(Tile tile)
        {
            throw new NotImplementedException();
        }

        private MemoryStream GetStreamFromBitmap(Bitmap bitmap)
        {
            MemoryStream memoryStream = new MemoryStream();
            EncoderParameter encoderParameter = null;
            EncoderParameters encoderParameters = null;

            try
            {
                if (ImageFormat == TileImageFormat.Jpeg)
                {
                    encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)JpegQuality);
                    encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = encoderParameter;
                    ImageCodecInfo jpegCodecInfo = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);

                    bitmap.Save(memoryStream, jpegCodecInfo, encoderParameters);
                }
                else if (ImageFormat == TileImageFormat.Png)
                {
                    bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                }
                else if (ImageFormat == TileImageFormat.Png8)
                {
                    var quantizer = new WuQuantizer();

                    using (var quantized = quantizer.QuantizeImage(bitmap))
                    {
                        quantized.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }

            }
            finally
            {
                if (encoderParameter != null) { encoderParameter.Dispose(); }
                if (encoderParameters != null) { encoderParameters.Dispose(); }
            }

            return memoryStream;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private int ExecuteSqlNonQuery(string SQLString, SQLiteParameter[] parameters = null)
        {
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                try
                {
                    connection.Open();
                    int count = cmd.ExecuteNonQuery();
                    return count;
                }
                catch (Exception)
                {
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        private Collection<GeoImage> GetImagesByColumnName(string SQLString, string columnName)
        {
            Collection<GeoImage> images = new Collection<GeoImage>();

            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    SQLiteDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        long bytesRead;
                        long fieldOffset = 0;
                        byte[] buffer = new byte[2048];
                        int columnIndex = sdr.GetOrdinal(columnName);

                        using (MemoryStream stream = new MemoryStream())
                        {
                            while ((bytesRead = sdr.GetBytes(columnIndex, fieldOffset, buffer, 0, buffer.Length)) > 0)
                            {
                                stream.Write(buffer, 0, (int)bytesRead);
                                fieldOffset += bytesRead;
                            }
                            images.Add(new GeoImage(new Bitmap(stream)));
                        }

                    }
                    return images;
                }
                catch (Exception)
                {
                    return images;
                }
                finally
                {

                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        private SQLiteConnection GetSQLiteConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = @"Data Source=" + defaultCacheFile;
                }

                return new SQLiteConnection(connectionString);
            }
            catch
            {
                return null;
            }
        }
    }
}
