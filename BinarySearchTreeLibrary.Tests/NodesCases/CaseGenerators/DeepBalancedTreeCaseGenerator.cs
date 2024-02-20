using BinarySearchTreeLibrary.Tests.NodesCases.FakeClass;

namespace BinarySearchTreeLibrary.Tests.NodesCases.CaseGenerators;

public static class DeepBalancedTreeCaseGenerator
{
	private static int[] _inputs = null!;
	public static IEnumerable<object[]> GetTreeCases()
	{
		_inputs = new[]
		{
			50, 25, 35, 75, 85, 65, 80, 15, 10, 20, 30, 90, 70
		};
		
		yield return new object[] {GetIntTestCase()};

		yield return new object[] {GetFakeClassTestCase()};
	}

	private static NodeCase GetIntTestCase()
	{
		return NodeCaseFactory.Create(_inputs.Cast<object>().ToArray());
	}

	private static NodeCase GetFakeClassTestCase()
	{
		var factory = new FakeClassFactory();

		var fakeClasses = new object[_inputs.Length];
		
		for(var i = 0; i < _inputs.Length; i++)
			fakeClasses[i] = factory.Create(_inputs[i]);
		
		return NodeCaseFactory.Create(fakeClasses.ToArray());
	}
}