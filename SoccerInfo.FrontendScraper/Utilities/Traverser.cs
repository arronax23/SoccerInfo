using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace SoccerInfo.FrontendScraper.Utilities;

public class Traverser(ILogger<Traverser> logger)
{
    private readonly List<HtmlNode> _foundNodes = new List<HtmlNode>();

    [Obsolete]
    public void Print()
    {
        foreach (var node in _foundNodes)
        {
            logger.LogInformation(node.InnerText);
        }
    }

    public IReadOnlyCollection<HtmlNode> Search(HtmlNode node, Predicate<HtmlNode> endSelectorsPredicate, Predicate<HtmlNode>? selectorsPredicate = null)
    {
        DFS(node, endSelectorsPredicate, selectorsPredicate);
        
        var result = _foundNodes.ToList();
        _foundNodes.Clear();

        return result;
    }

    private void DFS(HtmlNode node, Predicate<HtmlNode> endSelectorsPredicate, Predicate<HtmlNode>? selectorsPredicate = null)
    {
        if (node.NodeType == HtmlNodeType.Text)
        {
            return;
        }
        else if (selectorsPredicate is not null && selectorsPredicate(node))
        {
            _foundNodes.Add(node);
        }
        else if (endSelectorsPredicate(node))
        {
            _foundNodes.Add(node);
            return;
        }

        foreach (var child in node.ChildNodes)
        {
            DFS(child, endSelectorsPredicate, selectorsPredicate);
        }
    }
}
