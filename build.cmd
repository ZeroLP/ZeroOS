:: Disable command output
@echo OFF

:: Set variables
@set VS2022_DIRECTORY="C:\Program Files\Microsoft Visual Studio\2022\Community\"
@set ORIG_DRIVE=%CD:~0,2%
@set ORIG_DIRECTORY=%CD%
@set VHD="%CD%\ZeroOS.vhdx"
@set VDI="%CD%\ZeroOS.vdi"
@set VHD_SCRIPT="%CD%\diskpart.txt"
@set HYPER-V_DIRECTORY="C:\ZeroOSTestVM"

:: Delete existing files that are needed to be overwritten
@del %VHD% >nul 2>&1
@del %VHD_SCRIPT% >nul 2>&1
@del ZeroOS.ilexe >nul 2>&1
@del ZeroOS.obj >nul 2>&1
@del ZeroOS.map >nul 2>&1
@del ZeroOS.pdb >nul 2>&1
@del BOOTX64.EFI >nul 2>&1
@del ZeroOS.vhdx >nul 2>&1

:: Clean up exiting files instead of building
@if "%1" == "clean" (
    exit /b
)

:: Change directory if current direcetory isnt C drive
if NOT "%ORIG_DRIVE%" == "C:" (
    C:
)

:: Check if Visual Studio 2022 Community exist
if NOT exist %VS2022_DIRECTORY% (
    echo Visual Studio 2022 Community does not exist.
    pause
    exit /b
)

:: Run Dev CMD for VS 2022
call %VS2022_DIRECTORY%"\Common7\Tools\VsDevCmd.bat"

:: Change to original drive
%ORIG_DRIVE%

:: Change to original directory
cd %ORIG_DIRECTORY%

:: Check for build requirements
@set ILCPATH=%DROPPATH%\tools
@if not exist %ILCPATH%\ilc.exe (
  echo The DROPPATH environment variable is not set. Required: %ILCPATH%\ilc.exe
  exit /B
)

@where csc >nul 2>&1
@if ERRORLEVEL 1 (
  echo csc.exe does not exist in the Developer Prompt directory.
  exit /B
)

if exist %HYPER-V_DIRECTORY% (
  powershell -command "Stop-VM -Name ZeroOSTestVM -TurnOff"
)

:: Run CSharp Compiler
csc /nologo /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 ZeroOS/Program.cs ZeroOS/BootLoader/EFI/CLI.cs ZeroOS/BootLoader/EFI/UEFIBaseType.cs ZeroOS/CLR/ILC_COMPLIANTS/Internal.Runtime.CompilerHelpers.cs ZeroOS/CLR/ILC_COMPLIANTS/System.cs ZeroOS/CLR/System.cs ZeroOS/CLR/System.Runtime.InteropServices.cs /out:ZeroOS.ilexe /langversion:latest /unsafe
%ILCPATH%\ilc ZeroOS.ilexe -o ZeroOS.obj --systemmodule ZeroOS --map ZeroOS.map -O
link /nologo /subsystem:EFI_APPLICATION ZeroOS.obj /entry:EfiMain /incremental:no /out:BOOTX64.EFI

:: Create and attach virtual disk
@(
  echo create vdisk file=%VHD% maximum=500
  echo select vdisk file=%VHD%
  echo attach vdisk
  echo convert gpt
  echo create partition efi size=100
  echo format quick fs=fat32 label="System"
  echo assign letter="X"
  echo exit
)>%VHD_SCRIPT%

:: Run disk part
diskpart /s %VHD_SCRIPT%

:: Archive EFI
xcopy BOOTX64.EFI X:\EFI\BOOT\

:: Detach virtual disk
@(
  echo select vdisk file=%VHD%
  echo select partition 2
  echo remove letter=X
  echo detach vdisk
  echo exit
)>%VHD_SCRIPT%

:: Run disk part
diskpart /s %VHD_SCRIPT%

:: Create Hyper-V VM if it doesn't exist
if NOT exist %HYPER-V_DIRECTORY% (
  powershell -command "New-VM -Name ZeroOSTestVM -Generation 2 -MemoryStartupBytes 1GB -BootDevice VHD -VHDPath '%VHD%' -Path '%HYPER-V_DIRECTORY%'" 
  powershell -command "Set-VMFirmware ZeroOSTestVM -EnableSecureBoot Off"  
)

:: Run Hyper-V VM 
powershell -command "Start-VM -Name ZeroOSTestVM"
powershell -command "vmconnect.exe localhost ZeroOSTestVM"

:: Wait for input and exit
pause
