using Bogus;

namespace BinarySearchTreeLibrary.Tests.NodesCases;

public static class RandomExtensions
{
	public static T Unique<T>(this Randomizer randomizer, Func<T> generator, Func<T, int> hash, HashSet<int> existingHashes)
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
	public static string RandomString(int minHash = int.MinValue, int maxHash = int.MaxValue)
	{
		var faker = new Faker();
		var randomString = faker.Random.String();

		if (randomString.GetHashCode() < minHash || randomString.GetHashCode() > maxHash)
		{
			return RandomString(minHash, maxHash);
		}

		return randomString;
	}
	}
