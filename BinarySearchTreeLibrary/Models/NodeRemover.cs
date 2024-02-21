using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class NodeRemover<T> : INodeRemover<T>
{
	private static readonly INodeExtremumFinder<T?> _minNodeAtRight = new NodeExtremumFinder<T?>();

	public void RemoveNode(INode<T?> node)
	{
		if (node.IsLeaf)
			RemoveLeafNode(node);
		
		else if (node.HasBothChildren is false)
			RemoveNodeWithSingleChild(node);
		
		else
			RemoveNodeWithTwoChildren(node);

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
			var newNode = new NullNode<T?>();
			NodeUtils<T>.ReplaceNode(node, newNode);
		}
	}

	private static void RemoveNodeWithSingleChild(INode<T?> node)
	{
		var child = node.Left is not NullNode<T> ? node.Left : node.Right;

		if (child is NullNode<T>)
			return;

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

		if (node.Parent is not null)
			NodeUtils<T>.UpdateHeightPropsUpwards(node.Parent);
	}
}