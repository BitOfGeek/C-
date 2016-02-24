using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
    class ReversedList<T>: IEnumerable<T>{

        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public T[] List { get; private set; }

        public void Add(T item){
            int i = 1;
            if (this.Capacity == 0)
            {
                this.Capacity = 16;
                this.List = new T[this.Capacity];
            }
            if (this.Count == this.Capacity/2)
            {
                this.Capacity = this.Capacity * 2;
            }
            var tempList = new T[this.Capacity];
            tempList[0] = item;
            this.Count++;
            foreach (T items in this.List)
            {
                tempList[i] = item;
                i++;
            }
            i = 0;
            this.List = null;
            this.List = new T[this.Capacity];
            foreach (T items in tempList)
            {
                this.List[i] = item;
                i++;
            }
            tempList = null;
            

        }

        public void Remove(int index) {
            int i;
            var tempList = new T[this.Capacity];
            for (i=0; i < index; i++)
            {
                tempList[i] = this.List[i];
            }

            for (int x=i; x < this.Count; x++ )
            {
                tempList[x] = this.List[x+1];
            }
            this.List = null;
            i = 0;
            foreach (T items in tempList)
            {
                this.List[i] = items;
                i++;
            }
        }
        
        static void Main(string[] args){
        
        
        }
    }
}
