using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Serialization;

namespace Projeto_RenalPrime.Web.Controllers
{
    public class WebScrapingController : Controller
    {

        //const string Url = "https://localhost:7199/";

        const string Url = "https://projeto.clinicarenalprime.com.br/Home/";

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            List<SearchResult> results = new List<SearchResult>();
            foreach (var link in GetLinkAPages())
            {
                var result = await StartCrawlweAsync(link, searchTerm);
                if(result is not null)
                {
                    results.AddRange(result);
                }
            }
            return Json(results);
        }
        List<string> GetLinkAPages()
        {
            string url = Url;
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var links = new List<string>();
            var anchorNodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (anchorNodes is not null)
            {
                foreach (var node in anchorNodes)
                {
                    var link = node.GetAttributeValue("href", string.Empty);
                    if (!string.IsNullOrEmpty(link))
                    {
                        var absoluteUrl = new Uri(new Uri(url), link).ToString();
                        links.Add(absoluteUrl);
                    }
                }
            }
            return links;
        }
        async Task<List<SearchResult>> StartCrawlweAsync(string url, string searchTerm)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.OptionDefaultStreamEncoding = Encoding.UTF8;
            htmlDocument.LoadHtml(html);

            //Obtém o título da página
            var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
            var title = titleNode is not null ? titleNode.InnerText : "Título não encontrado";

            //Busca todas as divs que contém o termo de pesquisa

            var divs = htmlDocument.DocumentNode.Descendants("h6")
                .Where(node => node.GetAttributeValue("class", "").Contains("line-height-person"))
                .ToList();
            List<SearchResult> results = new List<SearchResult>();
            foreach (var div in divs)
            {
                
                if (div.InnerText.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    int indexOfTerm = div.InnerText.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase);

                    //Define o inínio e o fim do trecho que será exibido
                    string contentSnippet = div.InnerText.Substring(indexOfTerm);
                    // Se o conteúdo for maior que 150 caracteres após o termo, cortar
                    if (contentSnippet.Length > 80)
                    {
                        contentSnippet = contentSnippet.Substring(0, 80) + "...";
                        contentSnippet = contentSnippet.Replace("\r\n", " ").Replace("\n"," ").Replace("\r"," ").Replace("  ", "").Trim();
                    }

                    // Retorna a tupla com o título, link e o conteúdo a partir do termo
                    title = title.Replace("Clínica Renal Prime - ", "");
                    results.Add(new()
                    {
                       
                        Title = title is not null ? title : "Título não encontrado",
                        Url = url is not null ? url : "Url não localizada",
                        Content = contentSnippet is not null ? contentSnippet : "Conteudo não localizado"
                    });
                    //return new Tuple<string, string, string>(title, url, contentSnippet);

                }
            }
            return results;

        }

    }
    public struct SearchResult
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
