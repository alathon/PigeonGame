using System.Collections.Generic;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Basic shoot instruction, sets a duration to keep weapons on for.
    /// </summary>
    public class ShootInstruction : Instruction
    {
        public int volleys = 1;
        public List<Weapon> weaponsToFire;

        public override InstructionType GetType()
        {
            return InstructionType.Shoot;
        }
    }
}