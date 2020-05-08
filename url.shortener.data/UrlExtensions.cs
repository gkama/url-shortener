using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace url.shortener.data
{
    public static class UrlExtensions
    {
        public static Uri TryGetUri(this string url)
        {
            Uri.TryCreate(url, UriKind.Absolute, out var uri);

            return uri;
        }

        public static GkamaUrlMetadata ParseUri(this string url)
        {
            Uri.TryCreate(url, UriKind.Absolute, out var uri);

            return uri == null
                ? null
                : new GkamaUrlMetadata()
                {
                    Scheme = uri.Scheme,
                    Domain = uri.Host,
                    Port = uri.Port,
                    Path = uri.LocalPath,
                    Query = uri.Query,
                    Fragment = uri.Fragment
                };
        }

        public static GkamaUrl TryGetGkamaUrl(this string url)
        {
            Uri.TryCreate(url, UriKind.Absolute, out var uri);

            if (uri == null)
                throw new UrlException(HttpStatusCode.BadRequest, $"couldn't parse uri={uri}");

            var metadata = new GkamaUrlMetadata()
            {
                Scheme = uri.Scheme,
                Domain = uri.Host,
                Port = uri.Port,
                Path = uri.LocalPath,
                Query = uri.Query,
                Fragment = uri.Fragment
            };

            return new GkamaUrl()
            {
                Target = uri.AbsoluteUri,
                Metadata = metadata
            };
        }
    }
}
