using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class NullNode<T> : INode<T>
{
	public T Data { get; set; } = default!;
	public int Key => 0;
	public INode<T>? Left { get; set; } = null;
	public INode<T>? Right { get; set; } = null;
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
		}
	}

	public bool Insert(T data)
	{
		return false;
	}

	public INode<T> FindByKey(int key)
	{
		return this;
	}

	public INode<T> Remove(int key)
	{
		return this;
	}

	public INode<T> Balance()
	{
		return this;
	}
}