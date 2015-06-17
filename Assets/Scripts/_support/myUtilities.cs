using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public enum spawnDirection
{
	rot1=0,
	rot2=90,
	rot3=180,
	rot4=270
}
;

public static class myUtilities
{

/// <summary>
/// Gets the random enum from the type input
/// </summary>
/// <returns>The random enum.</returns>
/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T getRandomEnum<T> ()
	{
		System.Array A = System.Enum.GetValues (typeof(T));
		T v = (T)A.GetValue (UnityEngine.Random.Range (0, A.Length));
		
		return v;
	}
	
	/// <summary>
	/// Converts to string array.
	/// </summary>
	/// <returns>The to string array.</returns>
	/// <param name="A">A.</param>	
	public static string[] ConvertToStringArray (Object[] A)
	{
		string[] B = A.Where (x => x != null).Select (x => x.ToString ()).ToArray ();
		return B;
	}
	
	/// <summary>
	/// Shuffle the specified list.
	/// </summary>
	/// <param name="list">List.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void Shuffle<T> (this List<T> list)
	{  
		System.Random rng = new System.Random ();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next (n + 1);  
			T value = list [k];  
			list [k] = list [n];  
			list [n] = value;  
		}  
	}
		
	/// <summary>
	/// Sorts the given list
	/// </summary>
	/// <param name="list">List.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void SortMe<T> (this List<T> list)
	{
		list.Sort ((x, y) => string.Compare (x.ToString (), y.ToString ()));			
	}
		
	/// <summary>
	/// Finds the item A and remove from the list.
	/// </summary>
	/// <param name="A">Object.</param>
	/// <param name="list">List.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void findAndRemove<T> (this List<T> list, T A)
	{
		if (list.Contains (A)) {
			list.Remove (A);
		}
	}
	
	/// <summary>
	/// Gets the total bounds.
	/// </summary>
	/// <returns>The total bounds.</returns>
	/// <param name="A">A.</param>
	public static Bounds getTotalBounds (Transform A)
	{	
		//getting the initial bound
		Bounds combinedBounds = A.GetComponent<Renderer> () ? (A.GetComponent<Renderer> ()).bounds : new Bounds (A.position, Vector3.zero);
		
		//iterating over all the childs in the hierarchy
		foreach (Transform grandChild in A) {
			var render = grandChild.GetComponent<Renderer> ();
			if (render)
				combinedBounds.Encapsulate (render.bounds);
				
			combinedBounds.Encapsulate (getTotalBounds (grandChild));
		}
		
		
		return combinedBounds;
	}
}
