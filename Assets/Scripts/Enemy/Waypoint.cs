using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [Serializable]
    public class Waypoint
    {
        public Vector2 Position;
        public float Speed = 1f;

        public Waypoint(Vector2 position, float speed)
        {
            this.Position = position;
            this.Speed = speed;
        }
    }
}