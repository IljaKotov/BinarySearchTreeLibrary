namespace BinarySearchTreeLibrary.Interfaces;

internal interface ITreeBalancer<T>
{
	INode<T?> Balance(INode<T?> node);
}