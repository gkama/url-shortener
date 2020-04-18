using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace url.shortener.data
{
    public class UrlException : ApplicationException
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }

        public UrlException(int StatusCode)
        {
            this.StatusCode = StatusCode;
        }

        public UrlException(string Message) : base(Message)
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public UrlException(int StatusCode, string Message) : base(Message)
        {
            this.StatusCode = StatusCode;
        }

        public UrlException(HttpStatusCode StatusCode, string Message) : base(Message)
        {
            this.StatusCode = (int)StatusCode;
        }

        public UrlException(int StatusCode, Exception Inner) : this(StatusCode, Inner.ToString()) { }
        public UrlException(HttpStatusCode StatusCode, Exception Inner) : this(StatusCode, Inner.ToString()) { }
        public UrlException(int StatusCode, JsonElement ErrorObject) : this(StatusCode, ErrorObject.ToString()) { this.ContentType = @"application/problem+json"; }
    }
}
