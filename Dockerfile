FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY /app/publish/url.shortener.core /app/
RUN ls
RUN cat appsettings.json
EXPOSE 80
ENTRYPOINT ["dotnet", "url.shortener.core.dll"]