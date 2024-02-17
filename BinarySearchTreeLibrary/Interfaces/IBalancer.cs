namespace BinarySearchTreeLibrary.Interfaces;

internal interface IBalancer<T>
{
	INode<T> Balance(INode<T> node);
}