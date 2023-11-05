using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shop.Tools
{
    public static  class Tools
    {
        public static string getCategoryID(string categoryName)
        {
            return Shop.Views.ItemViews.categories.Where(category => category.category_name == categoryName).ToList()[0].categoryID;
        }

        async public static Task<bool> IsImageUrl(string url)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                return response.Content.Headers.ContentType.MediaType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
        public static bool IsUrlValid(string text)
        {
            if (Uri.TryCreate(text, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return true;
            }
            return false;
        }
    }
}
