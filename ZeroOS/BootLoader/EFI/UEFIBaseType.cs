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
        }

        public enum EFI_COLORS : UInt64
        {
            EFI_BLACK = 0x00,
            EFI_BLUE = 0x01
        }

        public class ReturnTypes
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

            /// <summary>
            /// The operation completed successfully.
            /// </summary>
            public static UInt64 RETURN_SUCCESS => 0;

            /// <summary>
            /// The image failed to load.
            /// </summary>
            public static UInt64 RETURN_LOAD_ERROR => ENCODE_ERROR(1);

            /// <summary>
            /// The parameter was incorrect.
            /// </summary>
            public static UInt64 RETURN_INVALID_PARAMETER => ENCODE_ERROR(2);

            /// <summary>
            /// The operation is not supported.
            /// </summary>
            public static UInt64 RETURN_UNSUPPORTED => ENCODE_ERROR(3);

            /// <summary>
            /// The buffer was not the proper size for the request.
            /// </summary>
            public static UInt64 RETURN_BAD_BUFFER_SIZE => ENCODE_ERROR(4);

            /// <summary>
            /// The buffer was not large enough to hold the requested data.
            /// </summary>
            public static UInt64 RETURN_BUFFER_TOO_SMALL => ENCODE_ERROR(5);

            /// <summary>
            /// There is no data pending upon return.
            /// </summary>
            public static UInt64 RETURN_NOT_READY=> ENCODE_ERROR(6);

            /// <summary>
            /// The physical device reported an error while attempting the operation.
            /// </summary>
            public static UInt64 RETURN_DEVICE_ERROR => ENCODE_ERROR(7);

            /// <summary>
            /// The device can not be written to.
            /// </summary>
            public static UInt64 RETURN_WRITE_PROTECTED => ENCODE_ERROR(8);

            /// <summary>
            /// The resource has run out.
            /// </summary>
            public static UInt64 RETURN_OUT_OF_RESOURCES => ENCODE_ERROR(9);

            /// <summary>
            /// An inconsistency was detected on the file system causing the operation to fail.
            /// </summary>
            public static UInt64 RETURN_VOLUME_CORRUPTED => ENCODE_ERROR(10);

            /// <summary>
            /// There is no more space on the file system.
            /// </summary>
            public static UInt64 RETURN_VOLUME_FULL => ENCODE_ERROR(11);

            /// <summary>
            /// The device does not contain any medium to perform the operation.
            /// </summary>
            public static UInt64 RETURN_NO_MEDIA => ENCODE_ERROR(12);

            /// <summary>
            /// The medium in the device has changed since the last access.
            /// </summary>
            public static UInt64 RETURN_MEDIA_CHANGED => ENCODE_ERROR(13);

            /// <summary>
            /// The item was not found.
            /// </summary>
            public static UInt64 RETURN_NOT_FOUND => ENCODE_ERROR(14);

            /// <summary>
            /// Access was denied.
            /// </summary>
            public static UInt64 RETURN_ACCESS_DENIED => ENCODE_ERROR(15);

            /// <summary>
            /// The server was not found or did not respond to the request.
            /// </summary>
            public static UInt64 RETURN_NO_RESPONSE => ENCODE_ERROR(16);

            /// <summary>
            /// A mapping to the device does not exist.
            /// </summary>
            public static UInt64 RETURN_NO_MAPPING => ENCODE_ERROR(17);

            /// <summary>
            /// A timeout time expired.
            /// </summary>
            public static UInt64 RETURN_TIMEOUT => ENCODE_ERROR(18);

            /// <summary>
            /// The protocol has not been started.
            /// </summary>
            public static UInt64 RETURN_NOT_STARTED => ENCODE_ERROR(19);

            /// <summary>
            /// The protocol has already been started.
            /// </summary>
            public static UInt64 RETURN_ALREADY_STARTED => ENCODE_ERROR(20);

            /// <summary>
            /// The operation was aborted.
            /// </summary>
            public static UInt64 RETURN_ABORTED => ENCODE_ERROR(21);

            /// <summary>
            /// An ICMP error occurred during the network operation.
            /// </summary>
            public static UInt64 RETURN_ICMP_ERROR => ENCODE_ERROR(22);

            /// <summary>
            /// A TFTP error occurred during the network operation.
            /// </summary>
            public static UInt64 RETURN_TFTP_ERROR => ENCODE_ERROR(23);

            /// <summary>
            /// A protocol error occurred during the network operation.
            /// </summary>
            public static UInt64 RETURN_PROTOCOL_ERROR => ENCODE_ERROR(24);

            /// <summary>
            /// A function encountered an internal version that was incompatible with a version requested by the caller.
            /// </summary>
            public static UInt64 RETURN_INCOMPATIBLE_VERSION => ENCODE_ERROR(25);

            /// <summary>
            /// The function was not performed due to a security violation.
            /// </summary>
            public static UInt64 RETURN_SECURITY_VIOLATION => ENCODE_ERROR(26);

            /// <summary>
            ///  A CRC error was detected.
            /// </summary>
            public static UInt64 RETURN_CRC_ERROR => ENCODE_ERROR(27);

            /// <summary>
            /// The beginning or end of media was reached.
            /// </summary>
            public static UInt64 RETURN_END_OF_MEDIA => ENCODE_ERROR(28);

            /// <summary>
            /// The end of the file was reached.
            /// </summary>
            public static UInt64 RETURN_END_OF_FILE => ENCODE_ERROR(31);

            /// <summary>
            /// The language specified was invalid.
            /// </summary>
            public static UInt64 RETURN_INVALID_LANGUAGE => ENCODE_ERROR(32);

            /// <summary>
            /// The security status of the data is unknown or compromised and the data must be updated or replaced to restore a valid security status.
            /// </summary>
            public static UInt64 RETURN_COMPROMISED_DATA => ENCODE_ERROR(33);

            /// <summary>
            /// A HTTP error occurred during the network operation.
            /// </summary>
            public static UInt64 RETURN_HTTP_ERROR => ENCODE_ERROR(35);

            /// <summary>
            /// The string contained one or more characters that the device could not render and were skipped.
            /// </summary>
            public static UInt64 RETURN_WARN_UNKNOWN_GLYPH => ENCODE_WARNING(1);

            /// <summary>
            /// The handle was closed, but the file was not deleted.
            /// </summary>
            public static UInt64 RETURN_WARN_DELETE_FAILURE => ENCODE_WARNING(2);

            /// <summary>
            /// The handle was closed, but the data to the file was not flushed properly.
            /// </summary>
            public static UInt64 RETURN_WARN_WRITE_FAILURE => ENCODE_WARNING(3);

            /// <summary>
            /// The resulting buffer was too small, and the data was truncated to the buffer size.
            /// </summary>
            public static UInt64 RETURN_WARN_BUFFER_TOO_SMALL => ENCODE_WARNING(4);

            /// <summary>
            /// The data has not been updated within the timeframe set by local policy for this type of data.
            /// </summary>
            public static UInt64 RETURN_WARN_STALE_DATA => ENCODE_WARNING(5);

            /// <summary>
            /// The resulting buffer contains UEFI-compliant file system.
            /// </summary>
            public static UInt64 RETURN_WARN_FILE_SYSTEM => ENCODE_WARNING(6);
        }
    }
}
