using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class NodeRemover<T> : INodeRemover<T>
{
	private static readonly INodeExtremumFinder<T> _minNodeAtRight = new NodeExtremumFinder<T>();

	public void RemoveNode(INode<T> node)
	{
		if (node.IsLeaf)
			RemoveLeafNode(node);
		
		else if (node.HasBothChildren is false)
			RemoveNodeWithSingleChild(node);
		
		else
			RemoveNodeWithTwoChildren(node);

		NodeUtils<T>.UpdateHeightPropsUpwards(node);
	}

	private static void RemoveLeafNode(INode<T> node)
	{
		if (node.Parent is null)
		{
			node.OnRootDeleted();
		}
		else
		{
			NodeUtils<T>.ReplaceNode(node, null);
		}
	}

	private static void RemoveNodeWithSingleChild(INode<T> node)
	{
		var child = node.Left ?? node.Right;

		ArgumentNullException.ThrowIfNull(child);
		
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

	private void RemoveNodeWithTwoChildren(INode<T> node)
	{
		ArgumentNullException.ThrowIfNull(node.Right);
		
		var successor = _minNodeAtRight.FindMinAt(node.Right);
		node.Data = successor.Data;

		RemoveNode(successor);

		if (node.Parent is not null)
			NodeUtils<T>.UpdateHeightPropsUpwards(node.Parent);
	}
}