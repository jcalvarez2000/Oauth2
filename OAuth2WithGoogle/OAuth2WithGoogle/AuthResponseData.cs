namespace OAuth2WithGoogle
{
    public class AuthResponseData
    {
        public string AccessToken { get; set; }
        public string Scope { get; set; }
        public string AuthUser { get; set; }
        public string Prompt { get; set; }

        public AuthResponseData(string uriResponse)
        {
            string[] parameters = uriResponse.Split('?', '&');

            foreach (string parameter in parameters)
            {
                string[] parValues = parameter.Split('=');
                if (parValues.Length == 2)
                {
                    switch (parValues[0])
                    {
                        case "code":
                            this.AccessToken = parValues[1];
                            break;
                        case "scope":
                            this.Scope = parValues[1];
                            break;
                        case "authuser":
                            this.AuthUser = parValues[1];
                            break;
                        case "prompt":
                            this.Prompt = parValues[1];
                            break;
                    }
                }
            }
        }

        public string toString()
        {
            return $"Access Token: {this.AccessToken}\nScope: {this.Scope}\nAuth User: {this.AuthUser}\nPrompt: {this.Prompt}";
        }
    }
}
