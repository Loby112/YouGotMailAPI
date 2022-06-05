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
        private const string APIKey = "";
        private const string BaseUri = "https://api.mailgun.net/v3";
        private const string Domain = "mailgundomain";
        private const string SenderAddress = "email";
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
