using System;
using System.Collections.Generic;
using System.Text;

namespace HeatMap
{
    internal struct HeatPoint
    {
        public int X;
        public int Y;
        public byte Intensity;

        public HeatPoint(int x, int y, byte intensity)
        {
            X = x;
            Y = y;
            Intensity = intensity;
        }
    }
}
