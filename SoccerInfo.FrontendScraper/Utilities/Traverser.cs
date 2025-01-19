using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace SoccerInfo.FrontendScraper.Utilities;

public class Traverser(ILogger<Traverser> logger)
{
    private readonly List<HtmlNode> _foundNodes = new List<HtmlNode>();
    public IReadOnlyCollection<HtmlNode> FoundNodes => _foundNodes;

    public void Print()
    {
        foreach (var node in _foundNodes)
        {
            logger.LogInformation(node.InnerText);
        }
    }

    public void DFS(HtmlNode node, Predicate<HtmlNode> endSelectorsPredicate, Predicate<HtmlNode> selectorsPredicate)
    {
        //logger.LogInformation("Traversing");
        if (node.NodeType == HtmlNodeType.Text)
        {
            return;
        }
        else if (selectorsPredicate(node))
        {
            _foundNodes.Add(node);
        }
        else if (endSelectorsPredicate(node))
        {
            _foundNodes.Add(node);
            return;
        }

        //logger.LogInformation(node.OuterHtml);

        foreach (var child in node.ChildNodes)
        {
            DFS(child, endSelectorsPredicate, selectorsPredicate);
        }
    }


    public void DFS(HtmlNode node, Predicate<HtmlNode> endSelectorsPredicate)
    {
        //logger.LogInformation("Traversing");
        if (node.NodeType == HtmlNodeType.Text)
        {
            return;
        }
        else if (endSelectorsPredicate(node))
        {
            _foundNodes.Add(node);
            return;
        }

        //logger.LogInformation(node.OuterHtml);

        foreach (var child in node.ChildNodes)
        {
            DFS(child, endSelectorsPredicate);
        }
    }

    public void ClearFoundNodesList()
    {
        _foundNodes.Clear();
    }
}
