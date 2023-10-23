using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractGameObjectFactory : MonoBehaviour
{
    public abstract T Create<T>();
}