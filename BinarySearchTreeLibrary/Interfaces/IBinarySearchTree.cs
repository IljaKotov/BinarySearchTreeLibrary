namespace BinarySearchTreeLibrary.Interfaces;

public interface IBinarySearchTree<T>
{
	public int Size { get;  }
	public int Height { get;  }
	public int RootBalanceFactor { get;  }
	bool Insert(T data);
	bool Contains(T data);
	bool Remove(T data);
	bool Balance();
	bool IsBalanced();
	bool IsBinarySearchTree();
	bool Clear();
}