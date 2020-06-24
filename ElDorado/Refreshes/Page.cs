﻿using ElDorado.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ElDorado.Refreshes
{
    public class Page
    {
        private readonly HtmlNode _pageHtml;
        private readonly string _url;

        public string Url => _url;

        public IEnumerable<Link> InPostExternalLinks
        {
            get 
            {
                var linkNodesWithValidHref = _pageHtml?.SelectNodes("//body")?.Descendants("a")?.Where(n => n.Attributes["href"] != null && n.Attributes["href"].Value.StartsWith("http"));
                var validExternalLinks = linkNodesWithValidHref?.Where(n => n.Attributes["href"].Value.DomainName() != _url.DomainName());
                return validExternalLinks?.Select(n => new Link(n)) ?? Enumerable.Empty<Link>(); 
            }
        }

        public string Title => WebUtility.HtmlDecode(_pageHtml.SafeGetNodeText("//title"));
        public string Body =>  _pageHtml.SafeGetNodeText("//body");
        public IEnumerable<string> H1s => _pageHtml.SafeGetNodeCollectionText("//h1");

        public IEnumerable<string> H2s => _pageHtml.SafeGetNodeCollectionText("//h2");

        public Page(string rawHtml, string url)
        {
            _url = url;
            _pageHtml = rawHtml.AsHtml();
        }
    }
}
