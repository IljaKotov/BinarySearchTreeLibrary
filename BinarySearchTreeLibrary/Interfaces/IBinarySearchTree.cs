namespace BinarySearchTreeLibrary.Interfaces;

public interface IBinarySearchTree<T>
{
	public int Size { get; }
	public int Height { get; }
	public int RootBalanceFactor { get; }
	public object? Root { get; }
	bool Insert(T data);
	bool Contains(T data);
	bool Remove(T data);
	void Balance();
	bool IsBalanced();
	bool IsBinarySearchTree();
}