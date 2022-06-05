    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace YouGotMailAPI.EmailServices
{
    public class EmailService
    {
        private const string APIKey = "792c6e808c78387301751f9ff7a73985-100b5c8d-60ab1d03";
        private const string BaseUri = "https://api.mailgun.net/v3";
        private const string Domain = "sandbox164811c8e1294099be864f1411f44f23.mailgun.org";
        private const string SenderAddress = "YouGotA@Mail.com";
        private const string SenderDisplayName = "YouGotAMail";
        private const string Tag = "sampleTag";

        public static IRestResponse SendEmail(UserEmailOptions userEmailOptions)
        {

            RestClient client = new RestClient
            {
                BaseUrl = new Uri(BaseUri),
                Authenticator = new HttpBasicAuthenticator("api", APIKey)
            };

            RestRequest request = new RestRequest();
            request.AddParameter("domain", Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"{SenderDisplayName} <{SenderAddress}>");

            request.AddParameter("to", userEmailOptions.ToEmails);
            

            request.AddParameter("subject", userEmailOptions.Subject);
            request.AddParameter("html", userEmailOptions.Body);
            request.AddParameter("o:tag", Tag);
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
