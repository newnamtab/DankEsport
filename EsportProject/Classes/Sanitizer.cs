using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Classes
{
    public static class Sanitizer
    {
        public static string SanitizeText(string stringToSanitize)
        {

            //string sanitizedString = System.Text.RegularExpressions.Regex.Replace(stringToSanitize, "<script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // REMOVES ALL "<>"-tags!!!
            //string sanitizedString = System.Text.RegularExpressions.Regex.Replace(stringToSanitize, "<.*?>", String.Empty);
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(stringToSanitize);
            string sanitizedString = htmlDoc.DocumentNode.InnerText;

            return sanitizedString;
        }
    }
}
