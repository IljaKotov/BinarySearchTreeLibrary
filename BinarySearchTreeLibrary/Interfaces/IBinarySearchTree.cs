namespace BinarySearchTreeLibrary.Interfaces;

public interface IBinarySearchTree<T>
{
	public T? RootData { get; }
	public int Size { get; }
	public int Height { get; }
	public int RootBalanceFactor { get; }
	void Add(T data);
	bool Contains(T data);
	bool TryDelete(T data);
	void Balance();
	bool IsBalanced();
	bool IsBinarySearchTree();
}