using System.Collections.Generic;

using UnityEngine;

public struct Pool<T> where T : Component, IPoolable
{
	private Stack<T> available;
	private HashSet<T> ignored;
	private readonly T obj;

	public Pool(T obj)
	{
		available = new Stack<T>();
		ignored = new HashSet<T>();
		this.obj = obj;
	}

	// Retrieve object from pool (or create one, if none exist)
	public T Pull()
	{
		while (available.Count > 0)
		{
			T obj = available.Pop();

			if (!ignored.Contains(obj))
			{
				obj.gameObject.SetActive(true);
				obj.OnPulledFromPool();
				return obj;
			}
		}

		Create();
		return Pull();
	}

	// Create new object
	private void Create()
	{
		T obj = Object.Instantiate(this.obj);
		available.Push(obj);
		obj.gameObject.SetActive(true);
	}

	// Push (add) object to pool
	public void Push(T obj)
	{
		available.Push(obj);
		obj.OnPushedToPool();
		obj.gameObject.SetActive(false);
	}

	// Ignore object when retrieving objects from pool
	public void Ignore(T obj)
	{
		ignored.Add(obj);
	}
}
