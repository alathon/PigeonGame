using UnityEngine;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Enum to define instruction type, currently moving, shooting and move-while-shooting are supported.
    /// </summary>
    public enum InstructionType
    {
        Move,
        Shoot,
        MoveAndShoot,
        Idle
    }

    /// <summary>
    /// Basic Instruction class that every instruction implementation must follow.
    /// </summary>
    public abstract class Instruction : MonoBehaviour
    {
        public new abstract InstructionType GetType();
    }
}
