using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T> //generic implementation of linkedlist
{
    protected Node<T> head;
    protected Node<T> tail;
    public int size;

    public LinkedList()
    {
        head = null;
        tail = null;
        size = 0;
    }

    public bool IsEmpty()
    {
        return size == 0;
    }

    public void AddToHead(T data)
    {
        Node<T> newNode = new Node<T>(data);

        newNode.Next = head;
        head = newNode;

        if (tail == null)
        {
            tail = head;
        }

        tail.Next = head;
        size++;
    }

    public void AddToTail(T data)
    {
        Node<T> newNode = new Node<T>(data);
        newNode.Next = null;

        if (tail != null)
        {
            tail.Next = newNode;
            tail = newNode;
            tail.Next = head;
        }
        else
        {
            head = tail = newNode;
        }

        size++;
    }

    public void DeleteFromHead()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("List is empty.");
        }

        Node<T> nodeToDelete = head;
        head = head.Next;
        tail.Next = head;
        size -= 1;

        if (IsEmpty())
        {
            tail = head = null;
        }
    }

    public void DeleteFromTail()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("List is empty.");
        }

        if (size == 1)
        {
            tail = head = null;
        }
        else
        {
            Node<T> currentNode = head;
            while (currentNode.Next != tail)
            {
                currentNode = currentNode.Next;
            }

            currentNode.Next = null;
            tail = currentNode;
            tail.Next = head;

        }
    }

    public Node<T> FindFirst(T data)
    {
        Node<T> currentNode = head;
        while (currentNode != null)
        {
            if (Equals(currentNode.Data, data))
            {
                return currentNode;
            }
            currentNode = currentNode.Next;
        }

        return null;
    }

    public void DeleteAtIndex(int index)
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("List is empty.");
        }

        if (index == 0)
        {
            DeleteFromHead();
            return;
        }

        Node<T> currentNode = head;
        for (int i = 0; i < index - 1; i++)
        {
            if (currentNode == null || currentNode.Next == null)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            currentNode = currentNode.Next;
        }

        Node<T> nodeToDelete = currentNode.Next;
        currentNode.Next = nodeToDelete.Next;
    }

    public T ElementAt(int index)
    {
        Node<T> currentNode = head;
        for (int i = 0; i < index; i++)
        {
            currentNode = currentNode.Next;
        }

        return currentNode.Data;
    }

    public void Clear()
    {
        head = null;
        size = 0;
    }

    public void Print()
    {
        Node<T> current = head;
        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}