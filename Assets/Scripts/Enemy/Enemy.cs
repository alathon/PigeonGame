using System;
using System.Collections.Generic;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    /// <summary>
    /// Enemy unit/AI.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        // Basic stats
        public Vector2 Position;     // Enemies position on screen.
        public float BaseSpeed = 5f; // Enemies base movement speed.
        public int Healthpoints = 100;

        // Navigation/AI related stuff.
        public List<Instruction> Instructions;             // List of Instructions the AI must carry out.
        public bool RepeatInstructionsOnCompletion = true; // Should the enemy reåeat the instruction list if it completes all?

        // Weapon related stuff.
        public WeaponSystem Weapons; // List of weapons the enemy carries.


        private int _instructionCounter = 0; // The next instruction to parse.
        private Rigidbody2D _rb2D;

        // Use this for initialization
        void Awake ()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }
	
        // Update is called once per physics frame
        void FixedUpdate () {
            // Parse the next instruction in the instruction list, if the instruction was not completed,
            // as if the destination of Move was not reached, just exit.
            var completedInstruction = ParseInstruction(Instructions[_instructionCounter]);
            if (!completedInstruction) return;

            // Otherwise increase instruction counter and take appropriate action.
            _instructionCounter++;
            if (_instructionCounter >= Instructions.Count && RepeatInstructionsOnCompletion)
            {
                // We went through the list and was told to stort over. Go to first instruction.
                _instructionCounter = 0;
            }
            else if (_instructionCounter >= Instructions.Count && !RepeatInstructionsOnCompletion)
            {
                // We went through the list but should not start over. Create an instruction to just stand still.
                Instructions.Clear();
                Instructions.Add(new MoveInstruction(transform.position));
                _instructionCounter = 0;
            }
        }

        private bool ParseInstruction(Instruction instruction)
        {
            switch (instruction.GetType())
            {
                case InstructionType.Move:
                    // Handle movement.
                    return Move((MoveInstruction) instruction);
                    break;
                case InstructionType.Shoot:
                    // Handle shooting.
                    break;
                case InstructionType.MoveAndShoot:
                    // Handle movement.
                    // Handle shooting.
                    break;
                default:
                    // Dafuq is this.
                    break;
            }
            return false;
        }

        private bool Move(MoveInstruction instruction)
        {
            // Calculate distance to waypoint
            var wp = instruction.GetCurrentWaypoint();
            var sqrRemainingDistance = (_rb2D.position - wp).sqrMagnitude;
            Vector2 newPostion;

            if (sqrRemainingDistance < float.Epsilon && instruction.HasNextWaypoint())
            {
                // If we're reasonably close and there is more waypoints, switch WP and move.
                instruction.SwitchWaypoint();
                wp = instruction.GetCurrentWaypoint();
                newPostion = Vector3.MoveTowards(_rb2D.position, wp, (1f / BaseSpeed) * Time.deltaTime);
                _rb2D.MovePosition(newPostion);
                return false;
            }
            if (sqrRemainingDistance < float.Epsilon && !instruction.HasNextWaypoint())
            {
                // If we're close but there is no WP, simply return, we'll move next tick.
                return true;
            }
            
            // We're not close, lets move!
            newPostion = Vector3.MoveTowards(_rb2D.position, wp, (1f / BaseSpeed) * Time.deltaTime);
            _rb2D.MovePosition(newPostion);
            return false;
        }
    }
}