using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Basic movement instruction, allows for setting a number of waypoints to follow.
    /// </summary>
    public class ShootInstruction : Instruction
    {
        public float Duration = 4f; // How many seconds to shoot for.

        public override InstructionType GetType()
        {
            return InstructionType.Shoot;
        }
    }
}