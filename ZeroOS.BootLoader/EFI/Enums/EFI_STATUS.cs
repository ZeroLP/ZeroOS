using System;

namespace ZeroOS.BootLoader.EFI.Enums
{
    public enum EFI_STATUS : UInt64
    {
        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        RETURN_SUCCESS = 0,

        /// <summary>
        /// The image failed to load.
        /// </summary>
        RETURN_LOAD_ERROR = (UInt64)(UInt64.MaxValue | (1)),

        /// <summary>
        /// The parameter was incorrect.
        /// </summary>
        RETURN_INVALID_PARAMETER = (UInt64)(UInt64.MaxValue | (2)),

        /// <summary>
        /// The operation is not supported.
        /// </summary>
        RETURN_UNSUPPORTED = (UInt64)(UInt64.MaxValue | (3)),

        /// <summary>
        /// The buffer was not the proper size for the request.
        /// </summary>
        RETURN_BAD_BUFFER_SIZE = (UInt64)(UInt64.MaxValue | (4)),

        /// <summary>
        /// The buffer was not large enough to hold the requested data.
        /// </summary>
        RETURN_BUFFER_TOO_SMALL = (UInt64)(UInt64.MaxValue | (5)),

        /// <summary>
        /// There is no data pending upon return.
        /// </summary>
        RETURN_NOT_READY = (UInt64)(UInt64.MaxValue | (6)),

        /// <summary>
        /// The physical device reported an error while attempting the operation.
        /// </summary>
        RETURN_DEVICE_ERROR = (UInt64)(UInt64.MaxValue | (7)),

        /// <summary>
        /// The device can not be written to.
        /// </summary>
        RETURN_WRITE_PROTECTED = (UInt64)(UInt64.MaxValue | (8)),

        /// <summary>
        /// The resource has run out.
        /// </summary>
        RETURN_OUT_OF_RESOURCES = (UInt64)(UInt64.MaxValue | (9)),

        /// <summary>
        /// An inconsistency was detected on the file system causing the operation to fail.
        /// </summary>
        RETURN_VOLUME_CORRUPTED = (UInt64)(UInt64.MaxValue | (10)),

        /// <summary>
        /// There is no more space on the file system.
        /// </summary>
        RETURN_VOLUME_FULL = (UInt64)(UInt64.MaxValue | (11)),

        /// <summary>
        /// The device does not contain any medium to perform the operation.
        /// </summary>
        RETURN_NO_MEDIA = (UInt64)(UInt64.MaxValue | (12)),

        /// <summary>
        /// The medium in the device has changed since the last access.
        /// </summary>
        RETURN_MEDIA_CHANGED = (UInt64)(UInt64.MaxValue | (13)),

        /// <summary>
        /// The item was not found.
        /// </summary>
        RETURN_NOT_FOUND = (UInt64)(UInt64.MaxValue | (14)),

        /// <summary>
        /// Access was denied.
        /// </summary>
        RETURN_ACCESS_DENIED = (UInt64)(UInt64.MaxValue | (15)),

        /// <summary>
        /// The server was not found or did not respond to the request.
        /// </summary>
        RETURN_NO_RESPONSE = (UInt64)(UInt64.MaxValue | (16)),

        /// <summary>
        /// A mapping to the device does not exist.
        /// </summary>
        RETURN_NO_MAPPING = (UInt64)(UInt64.MaxValue | (17)),

        /// <summary>
        /// A timeout time expired.
        /// </summary>
        RETURN_TIMEOUT = (UInt64)(UInt64.MaxValue | (18)),

        /// <summary>
        /// The protocol has not been started.
        /// </summary>
        RETURN_NOT_STARTED = (UInt64)(UInt64.MaxValue | (19)),

        /// <summary>
        /// The protocol has already been started.
        /// </summary>
        RETURN_ALREADY_STARTED = (UInt64)(UInt64.MaxValue | (20)),

        /// <summary>
        /// The operation was aborted.
        /// </summary>
        RETURN_ABORTED = (UInt64)(UInt64.MaxValue | (21)),

        /// <summary>
        /// An ICMP error occurred during the network operation.
        /// </summary>
        RETURN_ICMP_ERROR = (UInt64)(UInt64.MaxValue | (22)),

        /// <summary>
        /// A TFTP error occurred during the network operation.
        /// </summary>
        RETURN_TFTP_ERROR = (UInt64)(UInt64.MaxValue | (23)),

        /// <summary>
        /// A protocol error occurred during the network operation.
        /// </summary>
        RETURN_PROTOCOL_ERROR = (UInt64)(UInt64.MaxValue | (24)),

        /// <summary>
        /// A function encountered an internal version that was incompatible with a version requested by the caller.
        /// </summary>
        RETURN_INCOMPATIBLE_VERSION = (UInt64)(UInt64.MaxValue | (25)),

        /// <summary>
        /// The function was not performed due to a security violation.
        /// </summary>
        RETURN_SECURITY_VIOLATION = (UInt64)(UInt64.MaxValue | (26)),

        /// <summary>
        ///  A CRC error was detected.
        /// </summary>
        RETURN_CRC_ERROR = (UInt64)(UInt64.MaxValue | (27)),

        /// <summary>
        /// The beginning or end of media was reached.
        /// </summary>
        RETURN_END_OF_MEDIA = (UInt64)(UInt64.MaxValue | (28)),

        /// <summary>
        /// The end of the file was reached.
        /// </summary>
        RETURN_END_OF_FILE = (UInt64)(UInt64.MaxValue | (31)),

        /// <summary>
        /// The language specified was invalid.
        /// </summary>
        RETURN_INVALID_LANGUAGE = (UInt64)(UInt64.MaxValue | (32)),

        /// <summary>
        /// The security status of the data is unknown or compromised and the data must be updated or replaced to restore a valid security status.
        /// </summary>
        RETURN_COMPROMISED_DATA = (UInt64)(UInt64.MaxValue | (33)),

        /// <summary>
        /// A HTTP error occurred during the network operation.
        /// </summary>
        RETURN_HTTP_ERROR = (UInt64)(UInt64.MaxValue | (35)),

        /// <summary>
        /// The string contained one or more characters that the device could not render and were skipped.
        /// </summary>
        RETURN_WARN_UNKNOWN_GLYPH = (UInt64)1,

        /// <summary>
        /// The handle was closed, but the file was not deleted.
        /// </summary>
        RETURN_WARN_DELETE_FAILURE = (UInt64)2,

        /// <summary>
        /// The handle was closed, but the data to the file was not flushed properly.
        /// </summary>
        RETURN_WARN_WRITE_FAILURE = (UInt64)3,

        /// <summary>
        /// The resulting buffer was too small, and the data was truncated to the buffer size.
        /// </summary>
        RETURN_WARN_BUFFER_TOO_SMALL = (UInt64)4,

        /// <summary>
        /// The data has not been updated within the timeframe set by local policy for this type of data.
        /// </summary>
        RETURN_WARN_STALE_DATA = (UInt64)5,

        /// <summary>
        /// The resulting buffer contains UEFI-compliant file system.
        /// </summary>
        RETURN_WARN_FILE_SYSTEM = (UInt64)6
    }
}
