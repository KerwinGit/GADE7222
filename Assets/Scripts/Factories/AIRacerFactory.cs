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
    public abstract List <Material> material { get; }

    public abstract Mesh model { get; }
}

public class RedRacer : AIRacer
{
    public override int speed => 30;
    public override List<Material> material => new List<Material> { Resources.Load<Material>("Slowmat1"), Resources.Load<Material>("Slowmat2"), Resources.Load<Material>("Slowmat3"), Resources.Load<Material>("Slowmat4"), Resources.Load<Material>("Slowmat5"), Resources.Load<Material>("Slowmat6"), Resources.Load<Material>("Slowmat7") };
    public override Mesh model => Resources.Load<Mesh>("Slowcar");
}

public class GreenRacer : AIRacer
{
    public override int speed => 40;
    public override List<Material> material => new List<Material> { Resources.Load<Material>("RedMat1"), Resources.Load<Material>("RedMat2"), Resources.Load<Material>("RedMat3"), Resources.Load<Material>("RedMat4"), Resources.Load<Material>("RedMat5"), Resources.Load<Material>("RedMat6"), Resources.Load<Material>("RedMat7"), Resources.Load<Material>("RedMat8") };
    public override Mesh model => Resources.Load<Mesh>("Redcar");
}

public class BlueRacer : AIRacer
{
    public override int speed => 35;
    public override List<Material> material => new List<Material> { Resources.Load<Material>("Stubymat1"), Resources.Load<Material>("Stubymat2"), Resources.Load<Material>("Stubymat3"), Resources.Load<Material>("Stubymat4"), Resources.Load<Material>("Stubymat5"), Resources.Load<Material>("Stubymat6"), Resources.Load<Material>("Stubymat7") };
    public override Mesh model => Resources.Load<Mesh>("Stubby");
}

