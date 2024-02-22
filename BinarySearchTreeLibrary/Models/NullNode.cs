using BinarySearchTreeLibrary.Interfaces;

//namespace BinarySearchTreeLibrary.Models;

//internal class NullNode<T> : INode<T>
//{
	/*private T _data = default!;
	public T Data
	{
		get => throw new InvalidOperationException("Cannot access data on a NullNode.");
		set => _data = value;
	}
	public int Key => 0;
	public INode<T> Left { get; set; } = Instance;
	public INode<T> Right { get; set; } = Instance;
	public INode<T>? Parent { get; set; } = null;
	public int Height { get; set; } = -1;
	public bool IsLeaf => true;
	public bool HasBothChildren => false;

	public bool IsBalanced
	{
		get => true;
		set
		{
			/* Do nothing */
		/*}
	}

	private static NullNode<T> Instance { get; } = new();

	public void Insert(T data)
	{
	}

	public INode<T> FindByKey(int key)
	{
		return (INode<T>) this;
	}

	public INode<T> Remove(int key)
	{
		return this;
	}

	public INode<T> Balance()
	{
		return (INode<T>) this;
	}*/
//}