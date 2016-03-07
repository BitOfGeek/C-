using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayStack
{
    public class ArrayStack<T>
    {
        private T[] elements;
        public int Count { get; private set; }

        private const int InitialCapacity = 16;

        private int tail = 0;

        public ArrayStack(int capacity = InitialCapacity) {
            this.Count = 0;
            elements = new T[capacity];
        }
        public void Push(T element) {
            this.elements[this.Count]=element;
            this.Count++;
            if (this.Count == InitialCapacity / 2)
            {
                Grow();
            }
        }

        public T Pop() {
            var element = this.elements[this.Count];
            this.elements[this.Count] = default(T);
            return element;
        }
        public T[] ToArray() {
            int x = 0;
            var elementReturn= new T[InitialCapacity];
            for (int y = this.Count; y > -1; y--, x++)
            {
                elementReturn[x] = this.elements[y];
            }
                return elementReturn;
        }
        private void Grow() {
            var regrow = new T[InitialCapacity];
            for (int i = 0; i < this.Count; i++)
            {
                regrow[i] = this.elements[i];
            }
            var NewInitialCapacity= InitialCapacity*2;
            this.elements = new T[NewInitialCapacity];
            for (int i = 0; i < this.Count; i++)
            {
                elements[i] = regrow[i];
            }
        }

    }
}
