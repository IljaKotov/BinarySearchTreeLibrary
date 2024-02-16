using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases;

public class FakeClass : Faker
{
	private int FakeInt { get; set; }
	private string FakeString { get; set; }
	private char[] FakeArray { get; }

	public FakeClass()
	{
		FakeInt = Random.Int();
		FakeString = Random.String();
		var size = Random.Int(5, 20);
		FakeArray = new char[size];

		for (var i = 0; i < size; i++)
			FakeArray[i] = Random.Char();
	}
}