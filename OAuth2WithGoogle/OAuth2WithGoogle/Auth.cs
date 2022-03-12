using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace OAuth2WithGoogle
{
    public class Auth
    {
        public string clientId;
        public string clientSecret;
        public string redirectURI;

        public Auth(string clientId, string clientSecret, string redirectURI)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.redirectURI = redirectURI;
        }

        private string GetAutenticationURI(string clientId, string redirectUri)
        {
            string scopes = "https://www.googleapis.com/auth/plus.login email";

            if (string.IsNullOrEmpty(redirectUri))
            {
                redirectUri = "urn:ietf:wg:oauth:2.0:oob";
            }
            return $"https://accounts.google.com/o/oauth2/auth?client_id={this.clientId}&redirect_uri={this.redirectURI}&scope={scopes}&response_type=code";                        
        }

        public void authenticate()
        {
            try
            {
                System.Diagnostics.Process.Start(this.GetAutenticationURI(clientId, redirectURI));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public HttpWebRequest exchangeCodeForTokenRequest(string authCode)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");

            string postData = $"code={authCode}&client_id={this.clientId}&client_secret={this.clientSecret}&redirect_uri={this.redirectURI}&grant_type=authorization_code";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };

            return request;
        }

        private AuthResponse exchangeCodeForTokenResponse(HttpWebRequest request)
        {
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseString);
            
            authResponse.created = DateTime.Now;
            authResponse.clientId = this.clientId;
            authResponse.secret = this.clientSecret;

            return authResponse;
        }

        public AuthResponse exchangeCodeForToken(string authCode)
        {
            HttpWebRequest request = this.exchangeCodeForTokenRequest(authCode);

            return this.exchangeCodeForTokenResponse(request);
        }

    }
}
