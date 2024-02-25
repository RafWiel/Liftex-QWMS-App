using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Helpers
{
    public partial class Tools
    {
        public static string BuildUrl(string basePath, Dictionary<string, string?> queryParams)
        {
            var uriBuilder = new UriBuilder(basePath)
            {
                Query = string.Join("&", queryParams
                    .Where(u => !string.IsNullOrEmpty(u.Value))
                    .Select(u => $"{u.Key}={u.Value}"))
            };

            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}
