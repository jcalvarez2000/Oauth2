using System;
using System.Text;

namespace OAuth2WithGoogle
{
    class Program
    {
        public const string clientId = "379057754866-b08mo7g17i3cfdubjisj0u97ch7b12f4.apps.googleusercontent.com";
        public const string clientSecret = "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";
        public const string redirectURI = "http://localhost";

        static public string readUrlFromConsole()
        {
            System.IO.Stream inputStream = Console.OpenStandardInput();
            byte[] bytes = new byte[1256];
            int outputLength = inputStream.Read(bytes, 0, 1256);
            char[] chars = Encoding.UTF7.GetChars(bytes, 0, outputLength);

            return new string(chars);
        }

        static void Main(string[] args)
        {
            Auth authentication = new Auth(clientId, clientSecret, redirectURI);

            authentication.authenticate();

            Console.WriteLine("After login with gmail account in the web browser, copy the URL that has been redirected:");

            string url = readUrlFromConsole();

            Console.WriteLine("\nAuthorization Response Data:");
            Console.WriteLine("============================");

            AuthResponseData authData = new AuthResponseData(url);

            Console.WriteLine(authData.toString());

            Console.WriteLine("\nGetting the token...");
            AuthResponse authResponse = authentication.exchangeCodeForToken(authData.AccessToken);

            Console.WriteLine("\nCalling to the API to get my user info....");
            PeopleResponseData peopleData = GooglePlusApi.getUserInfo(authResponse.refresh_token);

            Console.WriteLine("\nUser info:");
            Console.WriteLine("=========:");
            Console.WriteLine(peopleData.toString());

            Console.WriteLine("\nPress enter to end the program.");
            Console.ReadLine();
        }
    }
}
