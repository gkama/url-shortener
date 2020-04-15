FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster
COPY . /app
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "url.shortener.core.dll"]