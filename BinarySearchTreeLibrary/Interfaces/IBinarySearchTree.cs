namespace BinarySearchTreeLibrary.Interfaces;

public interface IBinarySearchTree<T> 
{
	void Insert(T data); 
	void Remove(T data); 
	bool Contains(T data); 
	bool IsBalanced(); 
	bool IsBinarySearchTree();
}