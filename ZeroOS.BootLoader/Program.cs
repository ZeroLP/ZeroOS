using System;
using ZeroOS.BootLoader.EFI;
using static ZeroOS.BootLoader.EFI.UEFIBaseType;

namespace ZeroOS.BootLoader
{
    public unsafe class Program
    {
        public static readonly byte[] X64RetRAXASM =
        {
            0x48, 0xC7, 0xC0, 0x05, 0x00, 0x00, 0x00,       //mov rax, 0x5
            0xC3                                            //ret
        };

        public static EFI_SYSTEM_TABLE* Global_EFI_SYSTEM_TABLE;

        public static void Main() { } //Dummy main method in order to meet the ILC's requirements.

        [System.Runtime.RuntimeExport("EfiMain")]
        public static UInt64 EfiMain(IntPtr imageHandle, EFI_SYSTEM_TABLE* systemTable)
        {
            Global_EFI_SYSTEM_TABLE = systemTable;

            CLI.Initialize(systemTable);
            CLI.WriteLine("Hello World!");

            void **bufferPtr;
            BootLoader.EFI.Enums.EFI_STATUS allocStatus = systemTable->BootServices->AllocatePool(2, 256 * (sizeof(UInt64)), out bufferPtr);

            if (allocStatus == BootLoader.EFI.Enums.EFI_STATUS.RETURN_SUCCESS)
            {
                CLI.WriteLine("Memory pool has been successfully allocated");

                if (bufferPtr != null)
                {
                    *(nint*)*bufferPtr = 0x69;

                    if (*(nint*)*bufferPtr == 0x69)
                        CLI.WriteLine("Successfully written byte");
                    else
                        CLI.WriteLine("Failed to write byte");
                }
                else
                    CLI.WriteLine("bufferPtr is null");
            }
            else if (allocStatus == BootLoader.EFI.Enums.EFI_STATUS.RETURN_OUT_OF_RESOURCES)
                CLI.WriteLine("Failed to allocate buffer => RETURN_OUT_OF_RESOURCES");
            else if (allocStatus == BootLoader.EFI.Enums.EFI_STATUS.RETURN_INVALID_PARAMETER)
                CLI.WriteLine("Failed to allocate buffer => RETURN_INVALID_PARAMETER");
            else { }

            while (true) ; //Dangerous - return EFI_STATUS for safety based on conditions.
            //return ReturnTypes.RETURN_SUCCESS;
        }
    }
}
