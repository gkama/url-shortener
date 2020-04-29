﻿using System;
using System.Collections.Generic;
using System.Text;

namespace url.shortener.data
{
    public static class UrlExtensions
    {
        public static Uri TryGetUri(this string url)
        {
            Uri.TryCreate(url, UriKind.Absolute, out var uri);

            return uri;
        }

        public static IGkamaUrlMetadata ParseUri(this string url)
        {
            Uri.TryCreate(url, UriKind.Absolute, out var uri);

            return uri == null
                ? null
                : new GkamaUrlMetadata()
                {
                    Target = url,
                    Scheme = uri.Scheme,
                    Domain = uri.Host,
                    Port = uri.Port,
                    Path = uri.LocalPath,
                    Query = uri.Query,
                    Fragment = uri.Fragment
                };
        }
    }
}