
using System;
using System.Collections.Generic;
using Objects;
using RestSharp;
namespace API
{
    public abstract class ReqresApi
    {
        public static readonly string BaseUrl = "https://reqres.in/api/";
        protected RestClient restClient;
        public ReqresApi()
        {
            restClient = new(BaseUrl);
        }
    }
}
