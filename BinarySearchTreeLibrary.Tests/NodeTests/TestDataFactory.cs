using BinarySearchTreeLibrary.Interfaces;
using BinarySearchTreeLibrary.Models;

namespace BinarySearchTreeLibrary.Tests.NodeTests;

internal static class TestDataFactory
{
	public static INode<T> CreateNode<T>(T value) => new Node<T>(value);
	
	public static INode<T> InsertNodes<T>(INode<T> root, params T[] inputs)
	{
		for (var i = 1; i < inputs.Length; i++)
			root.Insert(inputs[i]);

		return root;
	}
}

