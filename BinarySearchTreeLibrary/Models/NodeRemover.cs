using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class NodeRemover<T> : INodeRemover<T>
{
	private readonly INodeExtremumFinder<T> _minNodeAtRight = new NodeExtremumFinder<T>();

	public void RemoveNode(INode<T?> node)
	{
		if (node.IsLeaf)
		{
			RemoveLeafNode(node);
		}
		else if (!node.HasBothChildren)
		{
			RemoveNodeWithSingleChild(node);
		}
		else
		{
			RemoveNodeWithTwoChildren(node);
		}

		NodeUtils<T>.UpdateHeightPropsUpwards(node);
	}

	private static void RemoveLeafNode(INode<T?> node)
	{
		if (node.Parent is null)
		{
			node.Data = default;
		}
		else
		{
			NodeUtils<T>.ReplaceNode(node, new NullNode<T>());
		}
	}

	private static void RemoveNodeWithSingleChild(INode<T?> node)
	{
		var child = node.Left is not NullNode<T> and not null ? node.Left : node.Right;

		if (child is NullNode<T> or null)
		{
			return;
		}

		child.Parent = node.Parent;

		if (node.Parent is not null)
		{
			NodeUtils<T>.ReplaceNode(node, child);

			return;
		}

		node.Data = child.Data;
		node.Left = child.Left;
		node.Right = child.Right;
	}

	private void RemoveNodeWithTwoChildren(INode<T?> node)
	{
		var successor = _minNodeAtRight.FindMinAt(node.Right);
		node.Data = successor.Data;

		RemoveNode(successor);
		NodeUtils<T>.UpdateHeightPropsUpwards(node.Parent);
	}
}