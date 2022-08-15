using System;

namespace ZeroOS.BootLoader.EFI.Enums
{
    public enum EFI_COLORS : UInt64
    {
        EFI_BLACK = 0x00,
        EFI_BLUE = 0x01,
        EFI_GREEN = 0x2,
        EFI_CYAN = (EFI_BLUE | EFI_GREEN),
        EFI_RED = 0x4,
        EFI_MAGENTA = (EFI_BLUE | EFI_RED),
        EFI_BROWN = (EFI_GREEN | EFI_RED),
        EFI_LIGHTGRAY = (EFI_BLUE | EFI_GREEN | EFI_RED),
        EFI_BRIGHT = 0x08,
        EFI_DARKGRAY = (EFI_BLACK | EFI_BRIGHT),
        EFI_LIGHTBLUE = (EFI_BLUE | EFI_BRIGHT),
        EFI_LIGHTGREEN = (EFI_GREEN | EFI_BRIGHT),
        EFI_LIGHTCYAN = (EFI_CYAN | EFI_BRIGHT),
        EFI_LIGHTRED = (EFI_RED | EFI_BRIGHT),
        EFI_LIGHTMAGENTA = (EFI_MAGENTA | EFI_BRIGHT),
        EFI_YELLOW = (EFI_BROWN | EFI_BRIGHT),
        EFI_WHITE = (EFI_BLUE | EFI_GREEN | EFI_RED | EFI_BRIGHT)
    }
}
