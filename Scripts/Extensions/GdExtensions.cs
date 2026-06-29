using System.Collections.Generic;
using Godot;

namespace CigMage.Extensions;

public static class GdExtensions
{
    /// <summary>
    /// Searches through children of a node to find all instances of type T.
    /// </summary>
    public static List<T> GetChildrenByType<T>(this Node node, bool recursive = true)
        where T : class
    {
        List<T> foundNodes = new();

        foreach (Node child in node.GetChildren())
        {
            if (child is T typedChild)
            {
                foundNodes.Add(typedChild);
            }

            if (recursive && child.GetChildCount() > 0)
            {
                foundNodes.AddRange(child.GetChildrenByType<T>());
            }
        }

        return foundNodes;
    }
    
    //acts like Unity's GetComponent<T> / GetComponentInChildren<T>
    public static T GetChildByType<T>(this Node node, bool recursive = true)
        where T : class
    {
        int childCount = node.GetChildCount();

        for (int i = 0; i < childCount; i++)
        {
            Node child = node.GetChild(i);
            if (child is T childT)
                return childT;

            if (recursive && child.GetChildCount() > 0)
            {
                T recursiveResult = child.GetChildByType<T>();
                if (recursiveResult != null)
                    return recursiveResult;
            }
        }

        return null;
    }

    //acts like Unity's GetComponentInParent<T>
    public static T GetParentByType<T>(this Node node)
        where T : class
    {
        Node parent = node.GetParent();
        if (parent != null)
        {
            if (parent is T parentT)
            {
                return parentT;
            }
            else
            {
                return parent.GetParentByType<T>();
            }
        }

        return null;
    }
}