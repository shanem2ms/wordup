dotnet build wordup.iOS/wordup.iOS.csproj -p:RuntimeIdentifier=ios-arm64 -p:_DeviceName=00008130-001A6C390221001C
ios-deploy --bundle wordup.iOS/bin/Debug/net8.0-ios/ios-arm64/wordup.iOS.app