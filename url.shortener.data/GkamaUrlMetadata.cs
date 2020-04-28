﻿using System;
using System.Collections.Generic;
using System.Text;

namespace url.shortener.data
{
    public class GkamaUrlMetadata : IGkamaUrlMetadata
    {
        public Guid PublicKey { get; set; }
        public string Target { get; set; }
        public string Scheme { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string Fragment { get; set; }
    }
}