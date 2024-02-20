namespace BinarySearchTreeLibrary.Interfaces;

internal interface INodeExtremumFinder<T>
{
	INode<T?> FindMinAt(INode<T?> node);
	INode<T?> FindMaxAt(INode<T?> node);
}