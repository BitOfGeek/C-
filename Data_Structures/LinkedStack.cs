using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedStack
{
    public class LinkedStack<T>
{
    private Node<T> firstNode;
    public int Count { get; private set; }
    public void Push(T element) {
        this.firstNode = new Node<T>(element, this.firstNode);
        this.Count++;
    }
    public T Pop() {
        var element = this.firstNode.value;
        this.firstNode = this.firstNode.NextNode;
        this.Count--;
        return element;
    }
    public T[] ToArray() {
        var item = this.firstNode;
        var array = new T[this.Count];
        int index = this.Count - 1;

        while (item != null)
        {
            array[index] = item.value;
            item = item.NextNode;
            index--;
        }
        return array;
    }
    private void Grow() {  }
    private class Node<T>
    {
        public T value;
        public Node<T> NextNode { get; set; }
        public Node(T value, Node<T> nextNode = null) {
            this.value = value;
            this.NextNode = nextNode;
        }
    }
}
    
}
