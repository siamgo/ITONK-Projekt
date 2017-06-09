FROM microsoft/aspnet
WORKDIR /app

# copy csproj and restore as distinct layers

COPY Provider.csproj .
RUN dotnet restore

# copy and build everything else

COPY . .

# RUN dotnet restore
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "out/Provider.dll", "--server.urls", "http://0.0.0.0:5000"]
