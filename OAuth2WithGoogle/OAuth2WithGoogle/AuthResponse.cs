using System;

namespace OAuth2WithGoogle
{
    public class AuthResponse
    {
        public string refresh_token { get; set; }
        public string clientId { get; set; }
        public string secret { get; set; }        
        public DateTime created { get; set; }
    }

}

