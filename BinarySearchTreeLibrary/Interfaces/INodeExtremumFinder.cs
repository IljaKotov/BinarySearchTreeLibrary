namespace BinarySearchTreeLibrary.Interfaces;

internal interface INodeExtremumFinder<T>
{
	INode<T> FindMinAt(INode<T> node);
}