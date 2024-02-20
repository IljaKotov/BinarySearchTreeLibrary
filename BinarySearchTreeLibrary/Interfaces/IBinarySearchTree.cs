namespace BinarySearchTreeLibrary.Interfaces;

public interface IBinarySearchTree<T>
{
	public int Size { get; }
	public int Height { get; }
	public int RootBalanceFactor { get; }
	public object? Root { get; }
	bool Add(T data);
	bool Contains(T data);
	bool Delete(T data);
	void Balance();
	bool IsBalanced();
	bool IsBinarySearchTree();
}