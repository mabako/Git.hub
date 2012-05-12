using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Serializers;

namespace Git.hub.util
{
    class ReplacingJsonSerializer : ISerializer
    {
        private JsonSerializer serializer = new JsonSerializer();
        private string what;
        private string with;

        public ReplacingJsonSerializer(string what, string with)
        {
            this.what = what;
            this.with = with;
        }

        public string Serialize(object obj)
        {
            return serializer.Serialize(obj).Replace(what, with);
        }

        public string ContentType
        {
            get
            {
                return serializer.ContentType;
            }
            set
            {
                serializer.ContentType = value;
            }
        }

        public string DateFormat
        {
            get
            {
                return serializer.DateFormat;
            }
            set
            {
                serializer.DateFormat = value;
            }
        }

        public string Namespace
        {
            get
            {
                return serializer.Namespace;
            }
            set
            {
                serializer.Namespace = value;
            }
        }

        public string RootElement
        {
            get
            {
                return serializer.RootElement;
            }
            set
            {
                serializer.RootElement = value;
            }
        }
    }
}
