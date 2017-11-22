set binaryPath=%CD%\..\bin\Release
set outputPath=%CD%\..\bin\ILMerge
IF NOT EXIST "%outputPath%" MKDIR "%outputPath%"
IF ["%programfiles(x86)%"]==[""] SET ilmergePath="%programfiles%\Microsoft\ILMerge"
IF NOT ["%programfiles(x86)%"]==[""] SET ilmergePath="%programfiles(x86)%\Microsoft\ILMerge"
%ilmergePath%\ilmerge /ndebug /target:winexe /out:"%outputPath%\RawDiskCopier.exe" "%binaryPath%\RawDiskCopier.exe" "%binaryPath%\DiskAccessLibrary.dll" "%binaryPath%\Utilities.dll"