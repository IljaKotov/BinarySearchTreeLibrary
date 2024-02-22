using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal static class Utils<T>
{
	public static void UpdateProperties(INode<T> node)
	{
		node.UpdateHeight();
		node.UpdateBalancingData();
	}

	public static void UpdatePropertiesUpwards(INode<T>? node)
	{
		while (node is not null)
		{
			UpdateProperties(node);
			node = node.Parent;
		}
	}

	public static void ReplaceNodes(INode<T> nodeToReplace, INode<T>? newNode)
	{
		if (nodeToReplace.Parent is null)
			return;

		if (nodeToReplace.Parent.Left == nodeToReplace)
			nodeToReplace.Parent.Left = newNode;
		else
			nodeToReplace.Parent.Right = newNode;
	}
}