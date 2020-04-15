FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . /app
EXPOSE 80
ENTRYPOINT ["dotnet", "url.shortener.core.dll"]