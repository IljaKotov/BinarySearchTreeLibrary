using BinarySearchTreeLibrary.Exceptions;
using BinarySearchTreeLibrary.Interfaces;

namespace BinarySearchTreeLibrary.Models;

internal class Node<T> : INode<T>
{
	private static readonly ITreeBalancer<T> _treeBalancer = new TreeBalancer<T>();
	private static readonly INodeRemover<T> _nodeRemover = new NodeRemover<T>();
	private static readonly ITreeGuide<T> _treeGuide = new TreeGuide<T>();

	public T Data { get; set; }
	public int Key => Data?.GetHashCode() ?? 0;
	public INode<T>? Left { get; set; }
	public INode<T>? Right { get; set; }
	public INode<T>? Parent { get; set; }
	public int Height { get; set; }
	public bool IsBalanced { get; set; } = true;
	public int BalanceFactor { get; set; }
	public bool IsLeaf => Left is null && Right is null;
	public bool HasBothChildren => Left is not null && Right is not null;

	public Node(T data)
	{
		ArgumentNullException.ThrowIfNull(data);
		Data = data;
	}

	private Node(T data, INode<T>? parent)
	{
		ArgumentNullException.ThrowIfNull(data);
		Data = data;
		Parent = parent;
	}

	public void Insert(T data)
	{
		var comparison = DefineDirection(data, this);
		DuplicateKeyException.ThrowIfSameKeys(comparison);

		var childToInsert = comparison is Direction.Left ? Left : Right;

		if (childToInsert is not null)
			childToInsert.Insert(data);
		else
			CreateChild(data, comparison);

		Utils<T>.UpdateProperties(this);
	}

	public INode<T>? GetNodeByKey(int key)
	{
		return _treeGuide.FindByKey(this, key);
	}

	public INode<T> Remove(int key)
	{
		var removalNode = _treeGuide.FindByKey(this, key);

		if (removalNode is null)
			return this;

		_nodeRemover.RemoveNode(removalNode);

		return this;
	}

	public INode<T> Balance()
	{
		return _treeBalancer.Balance(this);
	}

	public void UpdateHeight()
	{
		var (leftHeight, rightHeight) = GetChildHeights();

		Height = 1 + Math.Max(leftHeight, rightHeight);
	}

	public void UpdateBalancingData()
	{
		var (leftHeight, rightHeight) = GetChildHeights();
		BalanceFactor = leftHeight - rightHeight;

		var leftBalance = Left?.IsBalanced ?? true;
		var rightBalance = Right?.IsBalanced ?? true;
		IsBalanced = Math.Abs(BalanceFactor) <= 1 && leftBalance && rightBalance;
	}

	public void OnRootDeleted()
	{
		RootDeleted?.Invoke();
	}

	public event Action? RootDeleted;

	private void CreateChild(T data, Direction compareDirection)
	{
		if (compareDirection is Direction.Left)
			Left = new Node<T>(data, this);
		else
			Right = new Node<T>(data, this);
	}

	private (int leftHeight, int rightHeight) GetChildHeights()
	{
		var leftHeight = Left?.Height ?? -1;
		var rightHeight = Right?.Height ?? -1;

		return (leftHeight, rightHeight);
	}

	private static Direction DefineDirection(T data, INode<T> node)
	{
		ArgumentNullException.ThrowIfNull(data);

		return data.GetHashCode().CompareTo(node.Key) switch
		{
			> 0 => Direction.Right,
			< 0 => Direction.Left,
			_ => Direction.Same
		};
	}
}