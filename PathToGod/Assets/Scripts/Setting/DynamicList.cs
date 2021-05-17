using System;
using System.Collections.Generic;
namespace ProjectPratice
{
    public class DynamicList
    {
        private List<object> m_List = new List<object>();

        public int Count
        {
            get
            {
                return m_List.Count;
            }
        }

        public DynamicList(List<object> list = null)
        {
            if (list == null)
            {
                m_List = new List<object>();
            }
            else
            {
                m_List = list;
            }
        }
        public object this[int i]
        {
            get
            {
                return Get(i);
            }
            set
            {
                Add(value);
            }
        }
        public object Get(int i)
        {
            if (i < 0 || i >= m_List.Count)
            {
                return null;
            }
            return m_List[i];
        }

        public void Add(object item)
        {
            m_List.Add(item);
        }

        public void RemoveAt(int i)
        {
            if (i < 0 || i >= m_List.Count)
            {
                return;
            }
            m_List.RemoveAt(i);
        }

        public bool Remove(object item)
        {
            return m_List.Remove(item);
        }

        public bool Contains(object item)
        {
            return m_List.Contains(item);
        }

        public void Clear()
        {
            m_List.Clear();
        }

        public object[] ToArray()
        {
            return m_List.ToArray();
        }
    }
}
