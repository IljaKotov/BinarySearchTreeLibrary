namespace BinarySearchTreeLibrary.Interfaces;

internal interface ITreeGuide<T>
{
	INode<T> FindMinAt(INode<T> node);
	INode<T>? FindByKey(INode<T>? node, int key);
}