using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class LinkedListTest : MonoBehaviour {
    [SerializeField] bool badList;
    [SerializeField] bool extreme;
    [SerializeField] int iter;
    WaitForSeconds delay;
    Stopwatch stopwatch;

    // badList add 1x :(
    // mylist add 5x
 

    private void Start()
    {
        stopwatch = new Stopwatch();
    }


    private void Update()
    {
        stopwatch.Start();

        int i = iter - 1;
        if (extreme) i = 0;
        for (; i < iter; i++)
        {
            if (badList)
            {
            }
            else
            {
            }
        }

        Debug.Log(stopwatch.ElapsedMilliseconds);
        stopwatch.Stop();
        stopwatch.Reset();
    }

}


class LinkedList<T>
{

    public Node<T> head;
    public Node<T> last;
    public LinkedList(int count)
    {
        head = new Node<T>(null, default(T));
        Node<T> n = head;
        count--;

        for (int i=0; i<count; i++)
        {
            n.next = new Node<T>(null, default(T));
            n = n.next;
        }
        last = n;
    }

    public T GetData(int pos)
    {
        Node<T> n = head;
        for (int i=0; i<pos; i++)
        {
            if (n == null)
                return default(T);
            n = n.next;
        }
        return n.data;
    }

    public void Add(T data)
    {
        last.next = new Node<T>(null, data);
        last = last.next;
    }
    public Node<T> Remove(int pos)
    {
        Node<T> n = head;
        Node<T> m = new Node<T>(n,default(T));
        for (int i=0; i<pos; i++)
        {
            if (n == null)
                return null;
            m = n;
            n = n.next;
        }
        if (n == null)
            return null;

        m.next = n.next.next;
        n.next = null;

        return n;
    }
}

class Node<T>
{
    public Node(Node<T> next, T data)
    {
        this.next = next;
        this.data = data;
    }

    public Node<T> next;
    public T data;
}