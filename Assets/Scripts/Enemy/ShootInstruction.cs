using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Basic shoot instruction, sets a duration to keep weapons on for.
    /// </summary>
    public class ShootInstruction : Instruction
    {
        public float Duration = 4f; // How many seconds to shoot for.
        private float _timeRemaining = -1; // How many seconds do we need to shoot before completion?

        public override InstructionType GetType()
        {
            return InstructionType.Shoot;
        }

        void Awake()
        {
            _timeRemaining = Duration;
        }

        /// <summary>
        /// Signal that there have been shooting.
        /// The instruction gets reset if there is no more time left.
        /// </summary>
        /// <param name="seconds">The amount of time expended.</param>
        /// <returns>True if there is not more time left. False if there is more firing time left.</returns>
        public bool ShootFor(float seconds)
        {
            _timeRemaining -= seconds;
            if (_timeRemaining > 0) return false;
            _timeRemaining = Duration;
            return true;
        }
    }
}