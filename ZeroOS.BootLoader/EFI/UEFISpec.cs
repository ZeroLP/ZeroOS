using System;
using System.Runtime.InteropServices;
using ZeroOS.BootLoader.EFI;
using ZeroOS.BootLoader.EFI.Enums;

namespace ZeroOS.BootLoader.EFI
{
    public class UEFISpec
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct EFI_BOOT_SERVICES
        {
            public readonly UEFIBaseType.EFI_TABLE_HEADER Hdr;
            public readonly void* RaiseTPL;
            public readonly void* RestoreTPL;
            public readonly void* AllocatePAges;
            public readonly void* FreePages;
            public readonly void* GetMemoryMap;

            /// <summary>
            /// Allocates pool memory.
            /// </summary>
            public readonly delegate* unmanaged<UInt64, UInt64, out void**, EFI_STATUS> AllocatePool;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct EFI_RUNTIME_SERVICES
        {
            public readonly UEFIBaseType.EFI_TABLE_HEADER Hdr;

            /// <summary>
            /// Returns the current time and date information, and the time-keeping capabilities of the hardware platform.
            /// <para>[out] Time: A pointer to storage to receive a snapshot of the current time.</para>
            /// <para>[out] Capabilities: An optional pointer to a buffer to receive the real time clock device's capabilities.</para>
            /// </summary>
            public readonly delegate* unmanaged<out EFI_TIME*, out EFI_TIME_CAPABILITIES*, EFI_STATUS> GetTime;

            /// <summary>
            /// Sets the current local time and date information.
            /// <para>[in] Time: A pointer to the current time.</para>
            /// </summary>
            public readonly delegate* unmanaged<EFI_TIME*, EFI_STATUS> SetTime;

            /// <summary>
            /// Returns the current wakeup alarm clock setting.
            /// <para>[out] Enabled: Indicates if the alarm is currently enabled or disabled.</para>
            /// <para>[out] Pending: Indicates if the alarm signal is pending and requires acknowledgement.</para>
            /// <para>[out] Time: The current alarm setting.</para>
            /// </summary>
            public readonly delegate* unmanaged<out bool, out bool, out EFI_TIME*, EFI_STATUS> GetWakeupTime;

            /// <summary>
            /// Sets the system wakeup alarm clock time.
            /// <para>[in] Enable: Enable or disable the wakeup alarm.</para>
            /// <para>[in] Time: If Enable is TRUE, the time to set the wakeup alarm for. If Enable is FALSE, then this parameter is optional, and may be NULL.</para>
            /// </summary>
            public readonly delegate* unmanaged<bool, EFI_TIME*, EFI_STATUS> SetWakeupTime;

            /// <summary>
            /// Changes the runtime addressing mode of EFI firmware from physical to virtual.
            /// <para>[in] MemoryMapSize: The size in bytes of VirtualMap.</para>
            /// <para>[in] DescriptorSize: The size in bytes of an entry in the VirtualMap.</para>
            /// <para>[in] DescriptorVersion: The version of the structure entries in VirtualMap.</para>
            /// <para>[in] VirtualMap: An array of memory descriptors which contain new virtual address mapping information for all runtime ranges.</para>
            /// </summary>
            public readonly delegate* unmanaged<UInt64, UInt64, UInt32, EFI_MEMORY_DESCRIPTOR*, EFI_STATUS> SetVirtualAddressMap;

            /// <summary>
            /// Determines the new virtual address that is to be used on subsequent memory accesses.
            /// <para>[in] DebugDisposition: Supplies type information for the pointer being converted.</para>
            /// <para>[in,out] Address: A pointer to a pointer that is to be fixed to be the value needed for the new virtual address mappings being applied.</para>
            /// </summary>
            public readonly delegate* unmanaged<UInt64, ref void**, EFI_STATUS> ConvertPointer;

            /// <summary>
            /// Returns the value of a variable.
            /// <para>[in] VariableName: A Null-terminated string that is the name of the vendor's variable.</para>
            /// <para>[in] VendorGuid: A unique identifier for the vendor.</para>
            /// <para>[out] Attributes: If not NULL, a pointer to the memory location to return the attributes bitmask for the variable.</para>
            /// <para>[in,out] DataSize: On input, the size in bytes of the return Data buffer. On output the size of data returned in Data.</para>
            /// <para>[out] Data: The buffer to return the contents of the variable.May be NULL with a zero DataSize in order to determine the size buffer needed.</para>
            /// </summary>
            public readonly delegate* unmanaged<char, UEFIBaseType.GUID, out UInt32, ref UInt64, out void*, EFI_STATUS> GetVariable;

            /// <summary>
            /// Enumerates the current variable names.
            /// <para>[in,out] VariableNameSize: The size of the VariableName buffer.</para>
            /// <para>[in,out] VariableName: On input, supplies the last VariableName that was returned by GetNextVariableName(). On output, returns the Nullterminated string of the current variable.</para>
            /// <para>[in,out] VendorGuid: On input, supplies the last VendorGuid that was returned by GetNextVariableName(). On output, returns the VendorGuid of the current variable.</para>
            /// </summary>
            public readonly delegate* unmanaged<ref UInt64, ref char, ref UEFIBaseType.GUID, EFI_STATUS> GetNextVariableName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct EFI_MEMORY_DESCRIPTOR
        {
            /// <summary>
            /// Type of the memory region.
            /// </summary>
            public readonly UInt32 Type;

            /// <summary>
            /// Physical address of the first byte of the memory region.
            /// <para>Must aligned on a 4 KB boundary.</para>
            /// </summary>
            public readonly UInt64 PhysicalStart;

            /// <summary>
            /// Virtual address of the first byte of the memory region.
            /// <para>Must aligned on a 4 KB boundary.</para>
            /// </summary>
            public readonly UInt64 VirtualStart;

            /// <summary>
            /// Number of 4KB pages in the memory region.
            /// </summary>
            public readonly UInt64 NumberOfPages;

            /// <summary>
            /// Attributes of the memory region that describe the bit mask of capabilities for that memory region, and not necessarily the current settings for that memory region.
            /// </summary>
            public readonly UInt64 Attribute;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct EFI_TIME
        {
            public readonly UInt16 Year;
            public readonly uint Month;
            public readonly uint Day;
            public readonly uint Hour;
            public readonly uint Minute;
            public readonly uint Second;
            public readonly uint Pad1;
            public readonly UInt32 Nanosecond;
            public readonly Int16 TimeZone;
            public readonly uint Daylight;
            public readonly uint Pad2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe readonly struct EFI_TIME_CAPABILITIES
        {
            /// <summary>
            /// Provides the reporting resolution of the real-time clock device in counts per second.
            /// </summary>
            public readonly UInt32 Resolution;

            /// <summary>
            /// Provides the timekeeping accuracy of the real-time clock in an error rate of 1E-6 parts per million.
            /// </summary>
            public readonly UInt32 Accuracy;

            /// <summary>
            /// A TRUE indicates that a time set operation clears the device's time below the Resolution reporting level.
            /// </summary>
            public readonly bool SetsToZero;
        }
    }
}
