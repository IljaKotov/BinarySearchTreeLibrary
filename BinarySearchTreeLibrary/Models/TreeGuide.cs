using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class TreeGuide<T> : ITreeGuide<T>
{
	public INode<T> FindMinAt(INode<T> node)
	{
		while (node.Left is not null)
			node = node.Left;

		return node;
	}

	public INode<T>? FindByKey(INode<T>? node, int key)
	{
		while (node is not null)
		{
			if (node.Key == key)
				return node;

			node = key < node.Key ? node.Left : node.Right;
		}

		return null;
	}
}