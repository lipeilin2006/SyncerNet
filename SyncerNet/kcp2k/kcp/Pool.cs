﻿// Pool to avoid allocations (from libuv2k & Mirror)
using System.Collections.Concurrent;

namespace kcp2k
{
	public class Pool<T>
	{
		// Mirror is single threaded, no need for concurrent collections
		readonly ConcurrentStack<T> objects = new ConcurrentStack<T>();

		// some types might need additional parameters in their constructor, so
		// we use a Func<T> generator
		readonly Func<T> objectGenerator;

		// some types might need additional cleanup for returned objects
		readonly Action<T> objectResetter;

		public Pool(Func<T> objectGenerator, Action<T> objectResetter, int initialCapacity)
		{
			this.objectGenerator = objectGenerator;
			this.objectResetter = objectResetter;

			// allocate an initial pool so we have fewer (if any)
			// allocations in the first few frames (or seconds).
			for (int i = 0; i < initialCapacity; ++i)
				objects.Push(objectGenerator());
		}

		// take an element from the pool, or create a new one if empty
		public T Take()
		{
			while (objects.Count > 0)
			{
				if (objects.TryPop(out T result)) return result;
			}
			return objectGenerator();
		}

		// return an element to the pool
		public void Return(T item)
		{
			objectResetter(item);
			objects.Push(item);
		}

		// clear the pool
		public void Clear() => objects.Clear();

		// count to see how many objects are in the pool. useful for tests.
		public int Count => objects.Count;
	}
}
