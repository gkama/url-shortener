# Overview

Modern URL shortener

## Stack

- ASP.NET Core 3.1
- EntityFrameworkCore with PostgreSQL
- Docker (docker-compose)

## How it works

`/url/shorten` - all you need to specify is the target URL that needs to get shortened. Example:

Request

``` json
{
  "target": "https://google.com/gokasokdoakdaaaaw123o"
}
```

Response

``` json
{
  "id": 4,
  "publicKey": "fe2a2e83-b666-4191-a6a9-17ca68f720aa",
  "createdAt": "2020-04-20T04:18:02.5759202Z",
  "target": "https://google.com/gokasokdoakdaaaaw123o",
  "shortUrl": "https://gkama.it/Ore3vvu17J"
}
```

Randomly generating a 10 character long alphanumeric string to append to the shorten url. This is done via the `RandomString()` method.
It uses `Span<T>` and `string.Create()` as the main methods of string creation

### Resources

- [Adam Sitnik - Span - Introduction](https://adamsitnik.com/Span/#introduction)