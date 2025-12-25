using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TqkLibrary.Utils
{
    public static class SystemDrawingHelper
    {
        public static Point? GetCenter(this Rectangle? rectangle)
        {
            if (!rectangle.HasValue) return null;
            return rectangle.Value.GetCenter();
        }
        public static Point GetCenter(this Rectangle rectangle)
        {
            if (rectangle.IsEmpty) return Point.Empty;
            int x = rectangle.X + rectangle.Width / 2;
            int y = rectangle.Y + rectangle.Height / 2;
            return new Point(x, y);
        }
    }
}
