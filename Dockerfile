FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY . /app
WORKDIR /app
RUN ["dotnet", "publish"]
EXPOSE 80
ENTRYPOINT ["dotnet", "url.shortener.core.dll"]