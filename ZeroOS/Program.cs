using System;
using ZeroOS.BootLoader.EFI;
using static ZeroOS.BootLoader.EFI.UEFIBaseType;

namespace ZeroOS
{
    public unsafe class Program
    {
        public static EFI_SYSTEM_TABLE* Global_EFI_SYSTEM_TABLE;

        public static void Main() { } //Dummy main method in order to meet the ILC's requirements.

        [System.Runtime.RuntimeExport("EfiMain")]
        public static UInt64 EfiMain(IntPtr imageHandle, EFI_SYSTEM_TABLE* systemTable)
        {
            Global_EFI_SYSTEM_TABLE = systemTable;

            CLI.Initialize(systemTable);
            CLI.WriteLine("Hello World!");

            while (true) ; //Dangerous - return EFI_STATUS for safety based on conditions.
            //return ReturnTypes.RETURN_SUCCESS;
        }
    }
}
