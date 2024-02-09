namespace BinarySearchTreeLibrary.Interfaces;

internal interface INode<T>
{
	T Data { get; set; }
	int Key { get; set; }
	INode<T>? Left { get; set; }
	INode<T>? Right { get; set; }
	INode<T>? Parent { get; set; }
	int Height { get; set; }
	bool IsLeaf { get; }
	//bool IsLeftChild { get; }
	//bool IsRightChild { get; }
	bool HasBothChildren { get; }
	//bool HasLeftChild { get; }
	//bool HasRightChild { get; }
	//bool HasParent { get; }
	//bool IsRoot { get; }
	bool IsBalanced { get; }

	INode<T>? FindChild(int key);
	void ReplaceData(T newData);
	void ReplaceChild(INode<T> existingChild, INode<T>? newChild);
	bool Insert(T data);
	bool RemoveChild(int key);
	INode<T> Minimum();
	void ReplaceNode(INode<T>? newNode,  bool isRoot);
}