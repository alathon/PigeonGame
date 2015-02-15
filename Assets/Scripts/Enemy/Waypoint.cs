using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Waypoint
{
    public Vector2 Position;
    public float TimeToTarget = 1f;

    public Waypoint(Vector2 position, float timeToTarget)
    {
        this.Position = position;
        this.TimeToTarget = timeToTarget;
    }
}