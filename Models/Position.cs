using System.Runtime.InteropServices;
using System.Windows;

namespace NClicker.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point(Position point)
        {
            return new Point(point.X, point.Y);
        }
    }
}