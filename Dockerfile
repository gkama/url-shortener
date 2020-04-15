FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster
WORKDIR /app
COPY . /app
EXPOSE 80
ENTRYPOINT ["dotnet", "url.shortener.core.dll"]