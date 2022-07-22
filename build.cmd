@set ILCPATH=%DROPPATH%\tools
@if not exist %ILCPATH%\ilc.exe (
  echo The DROPPATH environment variable not set.
  exit /B
)
@where csc >nul 2>&1
@if ERRORLEVEL 1 (
  echo CSC not on the PATH.
  exit /B
)

@set VHD="%CD%\ZeroOS.vhdx"
@set VHD_SCRIPT="%CD%\diskpart.txt"
@del %VHD% >nul 2>&1
@del %VHD_SCRIPT% >nul 2>&1
@del ZeroOS.ilexe >nul 2>&1
@del ZeroOS.obj >nul 2>&1
@del ZeroOS.map >nul 2>&1
@del ZeroOS.pdb >nul 2>&1
@del BOOTX64.EFI >nul 2>&1

@if "%1" == "clean" exit /B

csc /nologo /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 ZeroOS/Program.cs ZeroOS/BootLoader/EFI/CLI.cs ZeroOS/BootLoader/EFI/UEFIBaseType.cs ZeroOS/CLR/ILC_COMPLIANTS/Internal.Runtime.CompilerHelpers.cs ZeroOS/CLR/ILC_COMPLIANTS/System.cs ZeroOS/CLR/System.cs ZeroOS/CLR/System.Runtime.InteropServices.cs /out:ZeroOS.ilexe /langversion:latest /unsafe
%ILCPATH%\ilc ZeroOS.ilexe -o ZeroOS.obj --systemmodule ZeroOS --map ZeroOS.map -O
link /nologo /subsystem:EFI_APPLICATION ZeroOS.obj /entry:EfiMain /incremental:no /out:BOOTX64.EFI

@rem Build a VHD if requested

@if not "%1" == "vhd" exit /B

if exist ZeroOS.vhdx (
	del ZeroOS.vhdx
)

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

diskpart /s %VHD_SCRIPT%

xcopy BOOTX64.EFI X:\EFI\BOOT\

@(
echo select vdisk file=%VHD%
echo select partition 2
echo remove letter=X
echo detach vdisk
echo exit
)>%VHD_SCRIPT%

diskpart /s %VHD_SCRIPT%