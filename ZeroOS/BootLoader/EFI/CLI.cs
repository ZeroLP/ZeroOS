﻿using System;
using static ZeroOS.BootLoader.EFI.UEFIBaseType;

namespace ZeroOS.BootLoader.EFI
{
    public unsafe class CLI
    {
        public static EFI_HANDLE CLIInputHandle;
        public static EFI_HANDLE CLIOutputHandle;

        public static void* CLIInProtocol;
        public static EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* CLIOutProtocol;

        public static void Initialize(EFI_SYSTEM_TABLE* systemTable)
        {
            CLIInputHandle = systemTable->ConsoleInHandle;
            CLIOutputHandle = systemTable->ConsoleOutHandle;

            CLIInProtocol = systemTable->ConIn;
            CLIOutProtocol = systemTable->ConOut;
        }

        public static void Reset()
        {
            if (CLIOutProtocol == null)
                CLIOutProtocol = Program.Global_EFI_SYSTEM_TABLE->ConOut;

            CLIOutProtocol->Reset(CLIOutProtocol, true);
        }

        public static bool TestString(string strToTest)
        {
            if (CLIOutProtocol == null)
                CLIOutProtocol = Program.Global_EFI_SYSTEM_TABLE->ConOut;

            UInt64 retVal = 0;

            fixed(char* pStr = strToTest)
                retVal = (ulong)CLIOutProtocol->TestString(CLIOutProtocol, pStr);

            return retVal == ReturnTypes.RETURN_SUCCESS;
        }

        public static void Write(string strToWrite)
        {
            if (CLIOutProtocol == null)
                CLIOutProtocol = Program.Global_EFI_SYSTEM_TABLE->ConOut;

            if (strToWrite == null || strToWrite.Length == 0)
            {
                string strNullOrLengthZeroErrorMessage = "Failed to write string as it was null or length was zero.";

                fixed (char* pStr = strNullOrLengthZeroErrorMessage)
                    CLIOutProtocol->OutputString(CLIOutProtocol, pStr);

                return;
            }

            fixed (char* pStr = strToWrite)
                CLIOutProtocol->OutputString(CLIOutProtocol, pStr);
        }

        public static void WriteLine(string strToWrite)
        {
            if (CLIOutProtocol == null)
                CLIOutProtocol = Program.Global_EFI_SYSTEM_TABLE->ConOut;

            if (strToWrite == null || strToWrite.Length == 0)
            {
                string strNullOrLengthZeroErrorMessage = "Failed to write string as it was null or length was zero.";

                fixed (char* pStr = strNullOrLengthZeroErrorMessage)
                    CLIOutProtocol->OutputString(CLIOutProtocol, pStr);

                return;
            }

            fixed (char* pStr = strToWrite)
                CLIOutProtocol->OutputString(CLIOutProtocol, pStr);

            fixed (char* pStr = "\r\n")
                CLIOutProtocol->OutputString(CLIOutProtocol, pStr);
        }

        public static void SetForeColor(UEFIBaseType.EFI_COLORS color)
        {
            if (CLIOutProtocol == null)
                CLIOutProtocol = Program.Global_EFI_SYSTEM_TABLE->ConOut;

            CLIOutProtocol->SetAttribute(CLIOutProtocol, (UInt64)color);
        }

        public static void Clear()
        {
            if (CLIOutProtocol == null)
                CLIOutProtocol = Program.Global_EFI_SYSTEM_TABLE->ConOut;

            CLIOutProtocol->ClearScreen(CLIOutProtocol);
        }

        public static void SetCursorPosition(UInt64 column, UInt64 row)
        {
            if (CLIOutProtocol == null)
                CLIOutProtocol = Program.Global_EFI_SYSTEM_TABLE->ConOut;

            CLIOutProtocol->SetCursorPosition(CLIOutProtocol, column, row);
        }
    }
}
