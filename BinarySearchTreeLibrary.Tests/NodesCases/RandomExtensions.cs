using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases;

public static class RandomExtensions
{
	public static T Unique<T>(this Randomizer randomizer,
		Func<T> generator,
		Func<T, int> hash,
		HashSet<int> existingHashes)
	{
		while (true)
		{
			var value = generator();
			var hashValue = hash(value);

			if (existingHashes.Contains(hashValue))
			{
				continue;
			}

			existingHashes.Add(hashValue);

			return value;
		}
	}
}