namespace BinarySearchTreeLibrary.Interfaces;

public interface IBinarySearchTree<T>
{
	public int Size { get; }
	public int Height { get; }
	public int RootBalanceFactor { get; }
	public T? RootData { get; }
	void Add(T data);
	bool Contains(T data);
	bool TryDelete(T data);
	void Balance();
	bool IsBalanced();
	bool IsBinarySearchTree();
}