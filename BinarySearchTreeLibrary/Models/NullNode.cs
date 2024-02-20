using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class NullNode<T> : INode<T>
{
	public T Data { get; set; } = default!;
	public int Key => 0;
	public INode<T>? Left { get; set; }
	public INode<T>? Right { get; set; }
	public INode<T>? Parent { get; set; }
	public int Height { get; set; }=-1;
	public bool IsLeaf => true;
	public bool HasBothChildren => false;
	public bool IsBalanced { get; private set; } = true;

	public bool Insert(T data)
	{
		return false;
	}

	public INode<T>? FindChild(int key)
	{
		return this;
	}
	
	public INode<T> Remove(int key)
	{
		return this;
	}

	public void UpdateHeight()
	{
	}

	public void UpdateBalanceFactor()
	{
	}

	public INode<T>? Rotate(bool isRight)
	{
		return this;
	}

	public INode<T>? RotateLeft()
	{
		return this;
	}

	public INode<T>? RotateRight()
	{
		return this;
	}

	public INode<T>? Balance()
	{
		return this;
	}
}