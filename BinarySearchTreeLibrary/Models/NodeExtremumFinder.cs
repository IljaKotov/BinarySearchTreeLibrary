using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class NodeExtremumFinder<T> : INodeExtremumFinder<T>
{
	public INode<T> FindMinAt(INode<T> node)
	{
		while (node.Left is not null)
			node = node.Left;

		return node;
	}
}