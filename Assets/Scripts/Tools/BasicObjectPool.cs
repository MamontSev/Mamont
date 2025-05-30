using System;
using System.Collections.Generic;

namespace Manmont.Tools
{
	public class BasicObjectPool<T>
	{
		private readonly Func<T> CreateFunc;
		private readonly Action<T> GetObject;
		private readonly Action<T> ReturnObject;

		private Queue<T> _pool;

		public BasicObjectPool( Func<T> CreateFunc, Action<T> GetObject , Action<T> ReturnObject,int defaultCount = 0 )
		{
			this.CreateFunc = CreateFunc;
			this.GetObject = GetObject;
			this.ReturnObject = ReturnObject;

			for( int i = 0; i < defaultCount; i++ )
			{
				ReturnObj(CreateFunc());
			}
		}

		public T GetObj()
		{
			T obj = _pool.Count > 0 ? _pool.Dequeue() : CreateFunc();
			GetObject(obj);

			return obj;
		}

		public void ReturnObj(T obj)
		{
			ReturnObject(obj);
			_pool.Enqueue(obj);
		}

	}
}
