using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIRacerFactory : AbstractFactory
{

    public override T Create<T>()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                return (T)(object)new RedRacer();
            case 1:
                return (T)(object)new GreenRacer();
            case 2:
                return (T)(object)new BlueRacer();
            default:
                return (T)(object)new GreenRacer();
        }
    }
}

public abstract class AIRacer
{
    public abstract int speed { get; }
    public abstract Material material { get; }
}

public class RedRacer : AIRacer
{
    public override int speed => 30;
    public override Material material => Resources.Load<Material>("Car Red");
}

public class GreenRacer : AIRacer
{
    public override int speed => 40;
    public override Material material => Resources.Load<Material>("Car Green");
}

public class BlueRacer : AIRacer
{
    public override int speed => 35;
    public override Material material => Resources.Load<Material>("Car Blue");
}

