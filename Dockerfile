FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
RUN ["dotnet", "publish"]
ENTRYPOINT ["dotnet", "url.shortener.core.dll"]