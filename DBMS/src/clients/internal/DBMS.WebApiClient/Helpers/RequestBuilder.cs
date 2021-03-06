﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.WebApiClient.Helpers
{
    class RequestBuilder
    {
        private StringBuilder _builder;
        private RequestBuilder(StringBuilder builder)
        {
            _builder = builder;
        }

        public static RequestBuilder StartBuild(string prefix)
        {
            return new RequestBuilder(new StringBuilder(prefix));
        }

        public RequestBuilder AddUrl(params string[] urls)
        {
            foreach(var url in urls)
            {
                _builder.Append("/");
                _builder.Append(url);
            }

            return this;
        }

        public RequestBuilder WithParams(params (string key, string value)[] parameters)
        {
            _builder.Append("?");
            foreach(var p  in parameters)
            {
                _builder.Append(p.key);
                _builder.Append("=");
                _builder.Append(p.value);
                _builder.Append("&");
            }

            return this;
        }

        public string Build() => _builder.ToString();
    }
}
