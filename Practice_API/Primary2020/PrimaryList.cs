using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Primary2020
{
	/// <summary>
	/// Summary description for PrimaryList.
	/// </summary>
	public class PrimaryList:PrimaryItem, IEnumerable
	{
		Hashtable _primaryList;
        List<PrimaryItem> _strongList;
		public int position;
		public PrimaryList()
		{
			//instantiate the primary list object
			_primaryList = new Hashtable();
            _strongList = new List<PrimaryItem>();
			//when performing a foreach loop the indexer is incremented
			//first. So in order to make it a zero-based indexed the position
			//is initialized as -1. Then ++position = 0
			position = -1;
		}

		public IEnumerator GetEnumerator()
		{
			return new PrimaryListEnumerator(this);
		}

		//returns the number of objects in the collection
		public int ListCount
		{
			get {return _primaryList.Count;}
		}

		public object this [string loc]
		{
			get {return (object) _primaryList[loc];}
		}

		public void Add (object _item, int loc)
		{
			_primaryList.Add(Convert.ToString(loc), _item);
		}
		public void Add (object _item)
		{
			_primaryList.Add(Convert.ToString(ListCount), _item);
            _strongList.Add((PrimaryItem)_item);
		}
		public bool MoveFirst()
		{
			if (ListCount > 0)
			{
				position = 0;
				return true;
			}
			else
			{
				position = -1;
				return false;
			}
		}
		public bool MoveNext()
		{
			if ((++position) >= (ListCount))
            {
				position = -1;
				return false;
			}
			else
				return true;
		}
		public object CurrentItem()
		{
			return (object) _primaryList[position.ToString()];
		}

		public object GetNext()
		{
			return (object) _primaryList[(position + 1).ToString()];
		}
        public List<PrimaryItem> GetPrimaryList()
        {
            return _strongList;
        }

        public PrimaryItem GetObjectByValue(string propertyName, object value)
        {
            position = -1;
            foreach (object item in this)
            {
                position++;
                PrimaryItem pItem = (PrimaryItem)item;
                PropertyInfo property = pItem.GetType().GetProperty(propertyName);
                Type type = Type.GetType(Convert.ToString(property.PropertyType));
                object o = property.GetValue(pItem, null);
                if (o.ToString() == value.ToString())
                    return pItem;
            }
            return null;
        }

		private class PrimaryListEnumerator:IEnumerator
		{
			PrimaryList eList;
			int location;
			public PrimaryListEnumerator (PrimaryList eList)
			{
				this.eList = eList;
				location = -1;
			}
			//Inorder to allow for iteration using the foreach loop
			//the IEnumerator interface comes
			//with three functions
			//these are MoveNext(), Current() and Reset()

			//the following function increments the indexer position
			//if indexer is not a valid location then it enters false
			//else true
			public bool 
				MoveNext()
			{
				++location;
				return (location > eList.ListCount - 1) ? false : true;
			}

			//returns the currently selected object
			public object Current
			{
				get
				{
					return eList[(string)Convert.ToString(location)];
				}
			}

			//reset the indexer. first thing called during a foreach loop
			public void Reset()
			{
				location = -1;
			}
		}
	}
}
