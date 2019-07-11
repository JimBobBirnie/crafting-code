using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftingCode
{
    public class MyStack
    {
        private List<object> _contents = new List<object>();
        public object Pop()
        {
            object item = _contents.Last();
            _contents.Remove(item);
            return item;
        }

        public void Push(object o)
        {
            _contents.Add(o);
        }
    }
}