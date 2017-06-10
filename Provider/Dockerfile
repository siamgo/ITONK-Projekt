FROM microsoft/aspnetcore:1.1
WORKDIR /app
COPY published ./
EXPOSE 80
ENTRYPOINT ["dotnet", "aspnetapp.dll"]