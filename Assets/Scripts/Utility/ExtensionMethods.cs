using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class ExtensionMethods {
	/// <summary>
	/// Picks a random element from an array.
	/// </summary>
	public static T GetRandom<T> (this T[] array) {
		int index = Random.Range (0, array.Length);
		
		return array[index];
	}
	
	/// <summary>
	/// Picks a random element from a collection.
	/// </summary>
	public static T GetRandom<T> (this IEnumerable<T> collection) {
		T[] array = collection.ToArray ();
		int index = Random.Range (0, array.Length);
		
		return array[index];
	}
	
	/// <summary>
	/// Picks a random element from a collection which is first filtered.
	/// </summary>
	public static T GetRandom<T> (this IEnumerable<T> collection, Func<T, bool> predicate) {
		T[] array = collection.Where (predicate).ToArray ();
		int index = Random.Range (0, array.Length);
		
		return array[index];
	}
	
	/// <summary>
	/// Check to see if an object contains a component.
	/// If not, then add that component.
	/// </summary>
	public static T GetOrAddComponent<T> (this Component child) where T: Component {
		T result = child.GetComponent<T>();
		if (result == null) {
			result = child.gameObject.AddComponent<T>();
		}
		return result;
	}

	/// <summary>
	/// Bootleg Intersect
	/// </summary>
	/// <returns>The common.</returns>
	/// <param name="first">First.</param>
	/// <param name="second">Second.</param>
	/// <typeparam name="TSource">The 1st type parameter.</typeparam>
	public static IEnumerable<TSource> GetCommon<TSource> (this IEnumerable<TSource> first, IEnumerable<TSource> second) {
		List<TSource> toReturn = new List<TSource> ();

		foreach (TSource i in first) {
			if (second.Contains (i))
				toReturn.Add (i);
		}

		return toReturn;
	}

	/// <summary>
	/// Bootleg Distinct
	/// </summary>
	/// <param name="source">Source.</param>
	/// <typeparam name="TSource">The 1st type parameter.</typeparam>
	public static IEnumerable<TSource> RemoveCopies<TSource> (this IEnumerable<TSource> source) {
		List<TSource> toReturn = new List<TSource> ();

		foreach (TSource i in source) {
			if (!toReturn.Contains (i))
				toReturn.Add (i);
		}

		return toReturn;
	}
}
