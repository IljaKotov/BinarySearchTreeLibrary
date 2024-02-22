namespace BinarySearchTreeLibrary.Interfaces;

internal interface INodeRemover<T>
{
	void RemoveNode(INode<T> removalNode);
}