FROM microsoft/aspnetcore
LABEL Name=provider Version=0.0.1 
WORKDIR /app
EXPOSE 3000
COPY . .
ENTRYPOINT ["dotnet", "Provider.dll"]