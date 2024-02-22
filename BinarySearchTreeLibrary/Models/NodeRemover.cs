using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class NodeRemover<T> : INodeRemover<T>
{
	private static readonly ITreeGuide<T> _treeGuide = new TreeGuide<T>();

	public void RemoveNode(INode<T> removalNode)
	{
		if (removalNode.IsLeaf)
			RemoveLeaf(removalNode);

		else if (removalNode.HasBothChildren is false)
			RemoveSingleChildNode(removalNode);

		else
			RemoveDoubleChildNode(removalNode);

		Utils<T>.UpdatePropertiesUpwards(removalNode);
	}

	private static void RemoveLeaf(INode<T> removalLeaf)
	{
		if (removalLeaf.Parent is null)
			removalLeaf.OnRootDeleted();
		else
			Utils<T>.ReplaceNodes(removalLeaf, null);
	}

	private static void RemoveSingleChildNode(INode<T> removalNode)
	{
		var inheritor = removalNode.Left ?? removalNode.Right;

		ArgumentNullException.ThrowIfNull(inheritor);

		inheritor.Parent = removalNode.Parent;

		if (removalNode.Parent is null)
		{
			removalNode.Data = inheritor.Data;
			removalNode.Left = inheritor.Left;
			removalNode.Right = inheritor.Right;
		}
		else
		{
			Utils<T>.ReplaceNodes(removalNode, inheritor);
		}
	}

	private void RemoveDoubleChildNode(INode<T> removalNode)
	{
		ArgumentNullException.ThrowIfNull(removalNode.Right);

		var inheritor = _treeGuide.FindMinAt(removalNode.Right);
		removalNode.Data = inheritor.Data;

		RemoveNode(inheritor);

		if (removalNode.Parent is not null)
			Utils<T>.UpdatePropertiesUpwards(removalNode.Parent);
	}
}