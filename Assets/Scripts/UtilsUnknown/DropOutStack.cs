using UnityEngine;
using System;

namespace UtilsUnknown
{
    //unfinished
    public class DropOutStack<T> where T : ICloneable
    {
        private T[] _data;
        private int _top;
        public int Count { get; private set; }

        private DropOutStack() { }

        public DropOutStack(int capacity)
        {
            _data = new T[capacity];
            _top = 0;
            Count = 0;
        }

        public void Clear()
        {
            _data = new T[_data.Length];
            _top = 0;
            Count = 0;
        }

        /*public bool Contains(T item)
        {
            int index = _top;
            for(int i = 0; i < Count; ++i)
            {

            }
            return false;
        }*/

        public T Peek()
        {
            if (Count < 1) throw new InvalidOperationException("The DropOutStack is empty.");
            return (T)_data[_top].Clone();
        }

        public T Pop()
        {
            if (Count < 1) throw new InvalidOperationException("The DropOutStack is empty.");
            Count -= 1;
            T ret = _data[_top];
            _top += _data.Length - 1;
            _top = _top % _data.Length;
            return ret;
        }

        public void Push(T item)
        {
            _top = (_top + 1) % _data.Length;
            Count = Mathf.Min(_data.Length, Count + 1);
            _data[_top] = item;
        }

    }
}
