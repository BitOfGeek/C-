using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedQueue
{
    public class LinkedQueue<T>
{
    private QueueNode<T> first = null;
    private QueueNode<T> last = null;
    public int Count { get; private set; }
    public void Enqueue(T element) {
        if (this.Count == 0)
        {
            this.first = new QueueNode<T>(element);
            this.last = this.first;
        }
        else
        {
            var tempLast = new QueueNode<T>(element);
            this.last.NextNode = tempLast;
            tempLast.PrevNode = this.last;
            this.last = tempLast;    
        }
        this.Count++;
    }
    public T Dequeue() {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty!");
        }
        T item = this.first.Value; ;
        if (this.first.NextNode != null)
        {
            this.first = this.first.NextNode;
            this.first.PrevNode = null;
        }
        else
        {
            this.first = null;
            this.last = null;
        }
        this.Count--;
        return item;
    }
    public T[] ToArray() {
        var array = new T[this.Count];
        var currentNode = this.first;
        int index = 0;
        while (currentNode != null)
        {
            array[index] = currentNode.Value;
            currentNode = currentNode.NextNode;
            index++;
        }

        return array;
    }

    private class QueueNode<T>
    {
        public T Value { get; private set; }
        public QueueNode<T> NextNode { get; set; }
        public QueueNode<T> PrevNode { get; set; }

        public QueueNode(T value, QueueNode<T> PrevNode = null)
        {
            this.Value = value;
            this.PrevNode = PrevNode;
        }
    }
}

}
