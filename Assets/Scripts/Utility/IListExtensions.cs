using System.Collections.Generic;

public static class IListExtensions {
	
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts) {
        int count = ts.Count;
        int last = count - 1;
        for (int i = last; i > 0; --i) {
            int r = XorRandom.GetInt(0,i+1);
            T tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

	/// <summary>
	/// Worsens the sorting by placing the best option last.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="ts"></param>
	public static void StupidSort<T>(this IList<T> ts) {
		int count = ts.Count;
		if (count <= 1)
			return;
		T tmp = ts[0];
		ts.RemoveAt(0);
		ts.Add(tmp);
	}
}
