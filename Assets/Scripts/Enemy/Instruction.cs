using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Enum to define instruction type, currently moving, shooting and move-while-shooting are supported.
    /// </summary>
    public enum InstructionType
    {
        Move,
        Shoot,
        MoveAndShoot
    }

    /// <summary>
    /// Basic Instruction class that every instruction implementation must follow.
    /// </summary>
    public abstract class Instruction : MonoBehaviour
    {
        public new abstract InstructionType GetType();
    }

    /// <summary>
    /// Basic movement instruction, allows for setting a number of waypoints to follow.
    /// </summary>
    public class MoveInstruction : Instruction
    {
        public Vector2[] Waypoints;

        public override InstructionType GetType()
        {
            return InstructionType.Move;
        }
    }
}
