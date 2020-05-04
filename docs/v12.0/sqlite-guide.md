# SQLite Guide

ThinkGeo now supports SQLite! This page is designed to give you some insight into why we wanted to support SQLite, how to use it, and details on how we implemented it. We also provide SQL statements to help you in the event you decide to modify a database outside of our APIs. This guide highlights some of the functionality but is not a replacement for the API documentation which provides a list of every method, property, and class associated with SQLite.

## Why SQLite
We had several priorities in mind while considering a suitable storage format for the immense amount of data contained in the world map kit.  First, we wanted an open format to allow developers to access data outside of our tools.  Second, the format had to be consolidated into a single file, while maintaining the ability to quickly delete unneeded data. We also wanted to make data analysis easy, and provide support for complex query style syntax. Last but not least, it had to be fast and perform well when loaded with lots of data. We chose SQLite as our back-end storage client, as it fit every aspect of what we wanted. Since SQLite is arguably the most widely deployed database engine, it has wide ranging support on just about every platform, including spatial index support.  SQLite allows us to provide a single database that can be easily queried and modified using free tools, and its source is public domain.

## Why Not SpatiaLite?

We originally started using SpatiaLite, which offered a number of additional GIS-related functions. However, it was not well supported and seemingly defunct. In the end we chose a plain implementation of SQLite to offer the greatest possible level of compatibility and supportability. ThinkGeo's API preserves the full range of functionality, making up for some losses in the database.

## SQLite Structure

While ThinkGeo has APIs for creating of all of the following tables, you may want to read on to understand what's going on behind the scenes.

We store data in SQLite using two main tables for each different feature type. The first table is where the actual tabular data and geometry is held, and it's typically named after the feature type it holds. For example, highways would be stored in a highway table. The second table is a virtual table that is linked to SQLite’s implementation on their spatial index (R-Tree). This virtual table hides a number of other tables that you never really see, so we won’t cover those here. We use the convention of naming the spatial index table `idx_{tablename}_{geometrycolumnname}`. So if the highway tables we mentioned earlier had a geometry column named "geometry", the index for that table and geometry column combination would be `idx_highway_geometry`. This system allows us to support multiple geometry columns for a table and provides some uniformity to the table structure.

The structure of the main table that stores the tabular data and geometry is straightforward. We require you to have a geometry column whose type is BLOB, and whose records contain the well-known binary related to that geometry. The tables also require a unique id column with the name of your choice. This id column will match each of these records to the spatial index, so it's imperative that it's unique, does not change, and is a number. SQLite requires a very specific format for the spatial index table, which is documented below. Note that you need to specify if the index will be for decimal degrees or for large integers. Included below is the create SQL along with the identical way to call it from ThinkGeo.

## SQLite APIs in ThinkGeo

### Create a New Database

```csharp
    // You can use the static method on the SqliteFeatureSource to create a new database file.
    SqliteFeatureSource.CreateDatabase(@"C:\sampledata\austin_streets.sqlite");
```

### Create a New Table

```csharp
    // We need to add an API to create a table and spatial index at once.
```

### Opening a Database

```csharp
    // We open the FeatureSource and pass in the database path along with the table name, id column and geometry column.
    // The same code is used to open the SqliteFeatureLayer.
    SqliteFeatureSource sqliteFeatureSource = new SqliteFeatureSource(@"Data Source=C:\sampledata\austin_streets.sqlite;Version=3;", "austin_streets", "id", "geometry");
    sqliteFeatureSource.Open();
```

### Inserting New Records

```csharp
    // Assuming the featuresource is opened, we begin a transaction, then add the new records and call the commit transaction.
    sqliteFeatureSource.BeginTransaction();
    sqliteFeatureSource.AddFeature(new Feature(10,20,"1"));
    sqliteFeatureSource.AddFeature(new Feature(5,15,"2"));
    sqliteFeatureSource.CommitTransaction();
```

### Editing Records

```csharp
    // Here we are updating feature 1 with a new set of coordinates.
    sqliteFeatureSource.BeginTransaction();
    sqliteFeatureSource.UpdateFeature(new Feature(10,10,"1"));
    sqliteFeatureSource.CommitTransaction();
```

### Deleting Records

```csharp
    // Here we are deleting features 1 and 2.
    sqliteFeatureSource.BeginTransaction();
    sqliteFeatureSource.DeleteFeature("1");
    sqliteFeatureSource.DeleteFeature("2");
    sqliteFeatureSource.CommitTransaction();
```

### Opening a `SQLiteFeatureLayer` For Display or Analysis

```csharp
    // We open the FeatureLayer and pass in the database path along with the table name, id column and geometry column.
    SqliteFeatureLayer sqliteFeatureLayer = new SqliteFeatureLayer(@"Data Source=C:\sampledata\austin_streets.sqlite;Version=3;", "austin_streets", "id", "geometry");
    sqliteFeatureLayer.Open();
```
 
## Sample SQLite Scripts

Be sure to wrap the operation in a transaction to ensure that the main table and index stay in sync.

### Create a New Table SQL

```sql
    -- This creates a table for decimal degrees.  Note the rtree part of the CREATE VIRTUAL TABLE
    BEGIN TRANSACTION;

    CREATE TABLE "austin_streets" ("id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, "geometry" BLOB, "name" TEXT);
    CREATE VIRTUAL TABLE "idx_austin_streets_geometry" USING rtree("id", "minx", "maxx", "miny", "maxy")

    COMMIT TRANSACTION;

    -- This creates a table for feet or meters.  Note the rtree_i32 part of the CREATE VIRTUAL TABLE
    BEGIN TRANSACTION;

    CREATE TABLE "austin_streets" ("id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, "geometry" BLOB, "name" TEXT);
    CREATE VIRTUAL TABLE "idx_austin_streets_geometry" USING rtree_i32("id", "minx", "maxx", "miny", "maxy")

    COMMIT TRANSACTION;
```

### Inserting New Records SQL

```sql
    -- We need to use an insert parameter here so we can add the geometry well known binary
    -- We also wrap this in a transaction so we can keep the main table and spatial index in sync
    BEGIN TRANSACTION;

    INSERT INTO "austin_streets" ("geometry","name","id") VALUES (@geometry,'Oak Street',1);
    INSERT INTO "idx_austin_streets_geometry" VALUES(1, -97.731584, -97.731192, 30.349088, 30.349304999999998)

    COMMIT TRANSACTION;
```

### Editing Records SQL

```sql
    --  Here we update both tables in case the geometry changed.  Also note that we are using a query parameter so we can pass in the geometry as well known binary
    BEGIN TRANSACTION;

    UPDATE "austin_streets" SET "geometry" = @geometry ,"name" = 'Red Oak Street' WHERE "id" = 1"
    UPDATE "idx_austin_streets_geometry" SET "minx" = -97.731584, "maxx" = -97.731192, "miny" = 30.349088, "maxy" = 30.349304999999998 WHERE "id" = 1"

    COMMIT TRANSACTION;
```

### Deleting Records SQL

```sql
    -- Here we delete the record from the main table and index.  Note that we do this in a transaction to keep them in sync.
    BEGIN TRANSACTION;

    DELETE FROM "austin_streets" WHERE "id" = '1';
    DELETE FROM "idx_austin_streets_geometry" WHERE "id" = '1';

    COMMIT TRANSACTION;
```

### Query a Table Based on an Extent

```sql
    --  Here we select all the record in the bounding box provided.
    SELECT "id", "geometry", "name" FROM "austin_streets" WHERE "austin_streets"."id" IN 
        (SELECT "id" FROM "idx_austin_streets_geometry" WHERE "minx" < -97.7340560913086 AND "maxx" > -97.7399658203125 AND "miny" < 30.294225692749 AND "maxy" > 30.288595199585);
```

## SQLite Dependencies

We use the unmanaged implementation of SQLite provided by the creators via a NuGet package. Specifically, we use the package `System.Data.SQLite Core (x86/x64)` because it has no other dependencies and supports both 32 and 64 bit application. When you add a reference to our `SQLiteExtension`, it will require you to add a reference to this NuGet package from your main application so that the unmanaged assemblies are included in the bin. As they are copied to the bin we support x-copy deployment and avoid potential conflict with other instances of SQLite that may exist on the machine.

[https://www.nuget.org/packages/System.Data.SQLite.Core/](https://www.nuget.org/packages/System.Data.SQLite.Core/)

## Management Tools

If you want to explore the data outside of ThinkGeo you can use any number of open and closed source tools to examine SQLite.  Below is a link to a list of the most popular tools.

[http://www.sqlite.org/cvstrac/wiki?p=ManagementTools](http://www.sqlite.org/cvstrac/wiki?p=ManagementTools)

A nice portable option is Database Browser Portable at the link below.

[http://portableapps.com/apps/development/database_browser_portable](http://portableapps.com/apps/development/database_browser_portable)
