using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWithVisualStudio.Tests
{
    public class Comparer
    {
        public static Comparer<U> Get<U>(Func<U, U, bool> func)
        {
            return new Comparer<U>(func);
        }

    }

    public class Comparer<T> : Comparer, IEqualityComparer<T>
    {
        private Func<T, T, bool> _comparsionFunction;

        public Comparer(Func<T, T, bool> func)
        {
            _comparsionFunction = func;
        }

        public bool Equals(T x, T y)
        {
            return _comparsionFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
