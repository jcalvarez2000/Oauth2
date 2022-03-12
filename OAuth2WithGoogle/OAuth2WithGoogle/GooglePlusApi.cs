using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace OAuth2WithGoogle
{
    public class GooglePlusApi
    {
        private static HttpWebRequest getUserInfoRequest(string accessToken)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/plus/v1/people/me");

            string postData = string.Format("access_token={0}", accessToken);

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

        public static PeopleResponseData getUserInfoResponse(HttpWebRequest request)
        {
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            PeopleResponseData result = JsonConvert.DeserializeObject<PeopleResponseData>(responseString);

            return result;
        }

        public static PeopleResponseData getUserInfo(string accessToken)
        {
            HttpWebRequest request = getUserInfoRequest(accessToken);

            return getUserInfoResponse(request);
        }

    }
}
