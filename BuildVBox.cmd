:: Disable command output
@echo OFF

:: Set variables
@set VS2022_DIRECTORY="C:\Program Files\Microsoft Visual Studio\2022\Community\"
@set ORIG_DRIVE=%CD:~0,2%
@set ORIG_DIRECTORY=%CD%
@set VHD="%CD%\ZeroOS.BootLoader.vhdx"
@set VDI="%CD%\ZeroOS.BootLoader.vdi"
@set VHD_SCRIPT="%CD%\diskpart.txt"
@set VIRTUALBOX_DIRECTORY="C:\Program Files\Oracle\VirtualBox"

:: Delete existing files that are needed to be overwritten
@del %VHD% >nul 2>&1
@del %VHD_SCRIPT% >nul 2>&1
@del ZeroOS.BootLoader.ilexe >nul 2>&1
@del ZeroOS.BootLoader.obj >nul 2>&1
@del ZeroOS.BootLoader.map >nul 2>&1
@del ZeroOS.BootLoader.pdb >nul 2>&1
@del BOOTX64.EFI >nul 2>&1

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

:: Run CSharp Compiler
csc /nologo /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 ZeroOS.BootLoader/Program.cs ZeroOS.BootLoader/EFI/CLI.cs ZeroOS.BootLoader/EFI/UEFIBaseType.cs ZeroOS.BootLoader/EFI/UEFISpec.cs ZeroOS.BootLoader/EFI/Enums/EFI_COLORS.cs ZeroOS.BootLoader/EFI/Enums/EFI_STATUS.cs ZeroOS.BootLoader/CLR/ILC_COMPLIANTS/Internal.Runtime.CompilerHelpers.cs ZeroOS.BootLoader/CLR/ILC_COMPLIANTS/Internal.TypeSystem.cs ZeroOS.BootLoader/CLR/ILC_COMPLIANTS/System.cs ZeroOS.BootLoader/CLR/System.cs ZeroOS.BootLoader/CLR/System.Runtime.InteropServices.cs /out:ZeroOS.BootLoader.ilexe /langversion:latest /unsafe
%ILCPATH%\ilc ZeroOS.BootLoader.ilexe -o ZeroOS.BootLoader.obj --systemmodule ZeroOS.BootLoader --map ZeroOS.BootLoader.map -O
link /nologo /subsystem:EFI_APPLICATION ZeroOS.BootLoader.obj /entry:EfiMain /incremental:no /out:BOOTX64.EFI

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

:: If VirtualBox exist,  Convert VHD to VDI, Create VM with it, and run the VM.
if exist %VIRTUALBOX_DIRECTORY% (
    :: Change directory if current direcetory isnt C drive
    if NOT "%ORIG_DRIVE%" == "C:" (
        C:
    )

    :: Change to VBOX directory
    cd %VIRTUALBOX_DIRECTORY%

    :: Stupid VirtualBox's UUID check won't let me overwrite VDI file.
    :: So just the entire VM
    if exist %UserProfile%"\VirtualBox VMs\ZeroOSTestVM"  (
      echo Existing VM found. Deleting...
      VBoxManage.exe unregistervm "ZeroOSTestVM" -delete
    )

    :: Convert VHDX to VDI
    VBoxManage.exe clonemedium disk %VHD% %VDI%

    ::Create VM
    VBoxManage.exe createvm --name "ZeroOSTestVM" --ostype "Other_64" --register

    ::Add SATA controller
    VBoxManage.exe storagectl "ZeroOSTestVM" --name "SATA Controller" --add sata --bootable on
    VBoxManage.exe storageattach "ZeroOSTestVM" --storagectl "SATA Controller" --port 0 --device 0 --type hdd --medium %VDI%

    :: Add RAM and VRAM
    VBoxManage.exe modifyvm "ZeroOSTestVM" --memory 1024 --vram 128

    ::Enable EFI
    VBoxManage.exe modifyvm "ZeroOSTestVM" --firmware efi

    VBoxManage.exe startvm "ZeroOSTestVM" --type gui
)

:: Wait for input and exit
pause
