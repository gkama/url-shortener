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

### GkamaURL Metadata

There is additional metadata that is stored in a separate table ccalled `gkama_url_metadata`. An example of such data is the domain, path, query, fragment, etc. It is extracted using C#'s `System.Uri` class. The logic converts the `string` URL to an `System.Uri` and extracts the information from it. Most of the fields are nullable as they're not always needed. Below is an example

``` json
{
  "id": 1,
  "urlId": 5,
  "publicKey": "d58bde73-dff6-457e-a2b3-f65eae2392e5",
  "createdAt": "2020-05-18T15:46:49.6928752Z",
  "scheme": null,
  "domain": "google.com",
  "port": 0,
  "path": null,
  "query": null,
  "fragment": null
}
```

### Resources

- [Adam Sitnik - Span - Introduction](https://adamsitnik.com/Span/#introduction)
