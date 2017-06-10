rmdir published -Recurse -Force
dotnet restore
dotnet build
dotnet publish -o published