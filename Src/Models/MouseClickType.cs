using System;

namespace NClicker.Models
{
    [Flags]
    public enum MouseClickType
    {
        LeftDown = 0x02,
        LeftUp = 0x04,
        RightDown = 0x08,
        RightUp = 0x10
    }
}