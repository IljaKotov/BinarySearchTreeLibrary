namespace BinarySearchTreeLibrary.Interfaces;

internal interface INode<T>
{
	T Data { get; set; }
	int Key { get; }
	INode<T>? Left { get; set; }
	INode<T>? Right { get; set; }
	INode<T>? Parent { get; set; }
	int Height { get; set; }
	bool IsBalanced { get; set; }
	int BalanceFactor { get; set; }
	bool IsLeaf { get; }
	bool HasBothChildren { get; }
	void Insert(T data);
	INode<T>? GetNodeByKey(int key);
	INode<T> Remove(int key);
	INode<T> Balance();
	void UpdateHeight();
	void UpdateBalancingData();
	void OnRootDeleted();
	event Action RootDeleted;
}