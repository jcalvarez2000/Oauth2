using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace OAuth2WithGoogle
{
    public class PeopleResponseData
    {
        public string Kind { get; set;}
        public string Etag { get; set; }
        public List<string> Items { get; set; }

        public string toString()
        {
            StringBuilder peopleResponseData = new StringBuilder($"Kind: {this.Kind}\nEtag: {this.Etag}\nItems: ");

            foreach (string item in this.Items)
            {
                peopleResponseData.Append(item);
            }
            return peopleResponseData.ToString();
        }

    }
}
