//https://dox.ipxe.org/UefiBaseType_8h_source.html

using System;
using System.Runtime.InteropServices;

namespace ZeroOS.BootLoader.EFI
{
    public static class UEFIBaseType
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct EFI_HANDLE
        {
            private IntPtr _handle;
        }

        [StructLayout(LayoutKind.Sequential)]
        public readonly struct EFI_TABLE_HEADER
        {
            public readonly ulong Signature;
            public readonly uint Revision;
            public readonly uint HeaderSize;
            public readonly uint Crc32;
            public readonly uint Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct EFI_SYSTEM_TABLE
        {
            public readonly EFI_TABLE_HEADER Hdr;
            public readonly char* FirmwareVendor;
            public readonly uint FirmwareRevision;
            public readonly EFI_HANDLE ConsoleInHandle;
            public readonly void* ConIn;
            public readonly EFI_HANDLE ConsoleOutHandle;
            public readonly EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* ConOut;
            public readonly void* StandardErrorHandle;
            public readonly EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* StdErr;
            public readonly UEFISpec.EFI_RUNTIME_SERVICES* RuntimeServices;
            public readonly UEFISpec.EFI_BOOT_SERVICES* BootServices;

        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL
        {
            /// <summary>
            /// Resets the text output device hardware. The cursor position is set to (0, 0), and the screen is cleared to the default background color for the output device
            /// </summary>
            public readonly delegate* unmanaged<void*, bool, void*> Reset;

            /// <summary>
            /// Writes a string to the output device.
            /// </summary>
            public readonly delegate* unmanaged<void*, char*, void*> OutputString;

            /// <summary>
            /// Verifies that all characters in a string can be output to the target device.
            /// </summary>
            public readonly delegate* unmanaged<void*, char*, void*> TestString;

            /// <summary>
            /// Returns information for an available text mode that the output device(s) supports.
            /// ModeNumber: The mode number to return information on. | Columns, Rows: Returns the geometry of the text output device for the request ModeNumber
            /// </summary>
            public readonly delegate* unmanaged<void*, UInt64, out UInt64, out UInt64, void*> QueryMode;

            /// <summary>
            /// Sets the output device(s) to a specified mode.
            /// </summary>
            public readonly delegate* unmanaged<void*, UInt64, void*> SetMode;

            /// <summary>
            /// Sets the background and foreground colors for the OutputString() and ClearScreen() functions.
            /// </summary>
            public readonly delegate* unmanaged<void*, UInt64, void*> SetAttribute;

            /// <summary>
            /// Clears the output device(s) display to the currently selected background color.
            /// </summary>
            public readonly delegate* unmanaged<void*, void*> ClearScreen;

            /// <summary>
            /// Sets the current coordinates of the cursor position.
            /// Column, Row: The position to set the cursor to. Must greater than or equal to zero and less than the number of columns and rows returned by QueryMode().
            /// </summary>
            public readonly delegate* unmanaged<void*, UInt64, UInt64, void*> SetCursorPosition;

            /// <summary>
            /// Makes the cursor visible or invisible. 
            /// </summary>
            public readonly delegate* unmanaged<void*, bool, void*> EnableCursor;
        }

        /// <summary>
        /// 128 bit buffer containing a unique identifier value.
        /// <para>Unless otherwise specified, aligned on a 64 bit boundary.</para>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct GUID
        {
            public readonly UInt32 Data1;
            public readonly UInt16 Data2;
            public readonly UInt16 Data3;
            public readonly uint Data4;
        }

        public class EFIStatusHelper
        {
            /// <summary>
            /// Produces a RETURN_STATUS(UInt64) code with the highest bit set.
            /// </summary>
            /// <param name="statusCode">The status code value to convert into a warning code. \nStatusCode must be in the range 0x00000000..0x7FFFFFFF.</param>
            /// <returns>The value specified by StatusCode with the highest bit set.</returns>
            public static UInt64 ENCODE_ERROR(UInt64 statusCode) => ((UInt64)(UInt64.MaxValue | (statusCode)));

            /// <summary>
            /// Produces a RETURN_STATUS(UInt64) code with the highest bit clear.
            /// </summary>
            /// <param name="statusCode">The status code value to convert into a warning code. StatusCode must be in the range 0x00000000..0x7FFFFFFF.</param>
            /// <returns>The value specified by StatusCode with the highest bit clear.</returns>
            public static UInt64 ENCODE_WARNING(UInt64 statusCode) => (UInt64)(statusCode);

            /// <summary>
            /// This function returns TRUE if StatusCode has the high bit set.  Otherwise, FALSE is returned.
            /// </summary>
            /// <param name="statusCode">The status code value to evaluate. </param>
            /// <returns>TRUE: The high bit of StatusCode is set. | FALSE: The high bit of StatusCode is clear.</returns>
            public bool RETURN_ERROR(UInt64 statusCode) => ((Int64)statusCode) < 0;
        }
    }
}
