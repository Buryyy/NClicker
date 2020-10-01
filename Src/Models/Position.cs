using System.Runtime.InteropServices;
using System.Windows;

namespace NClicker.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Position
    {
        public int X { get; }
        public int Y { get; }

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