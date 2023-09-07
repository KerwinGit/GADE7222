using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue<T> : LinkedList<T>
{
    public void Enqueue(T value)
    {
        AddToTail(value);
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        T front = head.Data;
        DeleteFromHead();
        return front;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        return head.Data;
    }

}
