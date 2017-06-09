FROM microsoft/dotnet:1.0-sdk
LABEL Name=provider Version=0.0.1 
ARG source=.
WORKDIR /app
EXPOSE 6000

COPY $source .

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

ENTRYPOINT ["dotnet", "run"]