using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Basic movement instruction, allows for setting a number of waypoints to follow.
    /// </summary>
    public class MoveInstruction : Instruction
    {
        public List<Vector2> Waypoints; // List of waypoints to fly through.
        private int _wpIndex = 0;

        /// <summary>
        /// Single constructor for code use. Mainly when just adding a single move instruction to an enemy.
        /// </summary>
        /// <param name="WayPoint">Waytpoint the instruction should travel to.</param>
        public MoveInstruction(Vector2 WayPoint)
        {
            Waypoints = new List<Vector2>();
            Waypoints.Add(WayPoint);
        }

        /// <summary>
        /// Returns the current waypoint.
        /// </summary>
        /// <returns>current waypoint as a Vector2</returns>
        public Vector2 GetCurrentWaypoint()
        {
            return Waypoints[_wpIndex];
        }

        /// <summary>
        /// Do we have more waypoints? 
        /// </summary>
        /// <returns>true if the navigation order have a next waypoint. False if the current waypoint was the last.</returns>
        public bool HasNextWaypoint()
        {
            return (_wpIndex < Waypoints.Count - 1);
        }

        /// <summary>
        /// Switches the current waypoint to the next. If the current waypoint is the last WP the current WP index will be set to 0.
        /// </summary>
        public void SwitchWaypoint()
        {
            _wpIndex = (_wpIndex + 1)%Waypoints.Count;
        }

        public override InstructionType GetType()
        {
            return InstructionType.Move;
        }
    }
}