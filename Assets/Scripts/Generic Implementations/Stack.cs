using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack<T> : LinkedList<T>//generic stack built off of inherits from linkedlist
{
    public void Push(T value)
    {
        AddToHead(value);
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        T top = head.Data;
        DeleteFromHead();
        return top;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        return head.Data;
    }

}
