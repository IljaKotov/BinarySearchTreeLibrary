namespace BinarySearchTreeLibrary.Interfaces;

internal interface INode<T>
{
	T Data { get; set; }
	int Key => Data != null ? Data.GetHashCode() : 0;
	INode<T>? Left { get; set; }
	INode<T>? Right { get; set; }
	INode<T>? Parent { get; set; }
	int Height { get; set; }
	bool IsLeaf { get; }
	bool HasBothChildren { get; }
	bool IsBalanced { get; }
	bool Insert(T data);
	INode<T>? FindChild(int key);
	INode<T> Remove(int key);
	INode<T>? Balance();
	INode<T>? RotateLeft();
	INode<T>? RotateRight();
	INode<T>? Rotate(bool isRight);
	void UpdateBalanceFactor();
	void UpdateHeight();
	int GetBalanceFactor();
}