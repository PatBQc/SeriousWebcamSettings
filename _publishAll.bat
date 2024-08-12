del .\Release\*.* /S /Q
del .\SeriousWebcamSettings\bin\Release\*.* /S /Q


REM .Net Version
dotnet publish -c Release
powershell -command "Compress-Archive -Path .\SeriousWebcamSettings\bin\Release\net6.0-windows\publish -DestinationPath .\Release\Windows-DotNet.zip"
del .\SeriousWebcamSettings\bin\Release\*.* /S /Q


REM Windows x64
dotnet publish -c Release -r win-x64 --self-contained
powershell -command "Compress-Archive -Path .\SeriousWebcamSettings\bin\Release\net6.0-windows\win-x64\publish\ -DestinationPath .\Release\Windows-x64-Self-Contained.zip"
del .\SeriousWebcamSettings\bin\Release\*.* /S /Q

REM Windows x86
dotnet publish -c Release -r win-x86 --self-contained
powershell -command "Compress-Archive -Path .\SeriousWebcamSettings\bin\Release\net6.0-windows\win-x86\publish\ -DestinationPath .\Release\Windows-x86-Self-Contained.zip"
del .\SeriousWebcamSettings\bin\Release\*.* /S /Q

REM Windows ARM 64 
dotnet publish -c Release -r win-arm64 --self-contained
powershell -command "Compress-Archive -Path .\SeriousWebcamSettings\bin\Release\net6.0-windows\win-arm64\publish\ -DestinationPath .\Release\Windows-ARM64-Self-Contained.zip"
del .\SeriousWebcamSettings\bin\Release\*.* /S /Q
