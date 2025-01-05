using HtmlAgilityPack;

namespace SoccerInfo.FrontendScraper.Utilities;
public static class NodeExtensions
{
    public static string? ExtractAttribute(this HtmlNode node, string attributeName)
    {
        var result = node.GetAttributeValue(attributeName, "notFound");

        if (result != "notFound") 
            return result;
        else 
            return null;
    }

    public static IEnumerable<HtmlNode> GetChildElementNodes(this HtmlNode node) 
        => node.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element);
}
