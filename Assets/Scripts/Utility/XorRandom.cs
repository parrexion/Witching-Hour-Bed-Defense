using System;
using System.Collections.Generic;


public static class XorRandom {


	private static ulong low;
	private static ulong high;

	public static void GenerateSeed() {
		Random r = new Random();
		low = (ulong)r.NextLong();
		high = (ulong)r.NextLong();
		//UnityEngine.Debug.Log("Seed is: " + low + "  :  " + high);
	}

	public static ulong GetNext() {
		if (low == 0 && high == 0) {
			UnityEngine.Debug.LogError("Seed is 0!");
			return 0;
		}
		ulong temp1 = low;
		ulong temp0 = high;
		low = temp0;
		temp1 ^= temp1 << 23; //a
		high = temp1 ^ temp0 ^ (temp1 >> 18) ^ (temp0 >> 5); //b, c
		return high + temp0;
	}

	/// <summary>
	/// Returns an int between lowRange(inclusive) and highRange(exclusive)
	/// </summary>
	/// <param name="lowRange"></param>
	/// <param name="highRange"></param>
	/// <returns></returns>
	public static int GetInt(int lowRange, int highRange) {
		ulong sides = (ulong)(highRange - lowRange);

		//Truncate uneveness
		ulong fullRange = ulong.MaxValue / sides;
		ulong maxRoll = sides * fullRange;

		ulong roll;
		do {
			roll = GetNext() % sides;
		} while(roll > maxRoll);
		return lowRange + (int)(roll % sides);
	}
}

public static class RandomExtensionMethods {
	/// <summary>
	/// Returns a random long from min (inclusive) to max (exclusive)
	/// </summary>
	/// <param name="random">The given random instance</param>
	/// <param name="min">The inclusive minimum bound</param>
	/// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
	public static long NextLong(this Random random, long min, long max) {
		if(max <= min)
			throw new ArgumentOutOfRangeException("max", "max must be > min!");

		//Working with ulong so that modulo works correctly with values > long.MaxValue
		ulong uRange = (ulong)(max - min);

		//Prevent a modolo bias; see https://stackoverflow.com/a/10984975/238419
		//for more information.
		//In the worst case, the expected number of calls is 2 (though usually it's
		//much closer to 1) so this loop doesn't really hurt performance at all.
		ulong ulongRand;
		do {
			byte[] buf = new byte[8];
			random.NextBytes(buf);
			ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
		} while(ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

		return (long)(ulongRand % uRange) + min;
	}

	/// <summary>
	/// Returns a random long from 0 (inclusive) to max (exclusive)
	/// </summary>
	/// <param name="random">The given random instance</param>
	/// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
	public static long NextLong(this Random random, long max) {
		return random.NextLong(0, max);
	}

	/// <summary>
	/// Returns a random long over all possible values of long (except long.MaxValue, similar to
	/// random.Next())
	/// </summary>
	/// <param name="random">The given random instance</param>
	public static long NextLong(this Random random) {
		return random.NextLong(long.MinValue, long.MaxValue);
	}
}
