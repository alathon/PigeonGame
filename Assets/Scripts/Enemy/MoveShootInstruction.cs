using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Basic movement while shooting instruction, allows for setting a number of waypoints to follow. Will fire weapons while moving.
    /// </summary>
    public class MoveShootInstruction : MoveInstruction
    {
        public override InstructionType GetType()
        {
            return InstructionType.MoveAndShoot;
        }
    }
}