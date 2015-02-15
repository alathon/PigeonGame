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
        public int Healthpoints = 100; // HP for enemy.

        // Navigation/AI related stuff.
        public List<Instruction> Instructions;             // List of Instructions the AI must carry out.
        public bool RepeatInstructionsOnCompletion = true; // Should the enemy reåeat the instruction list if it completes all?

        // Weapon related stuff.
        public WeaponSystem Weapons; // List of weapons the enemy carries.
        private bool _shooting = false;


        private int _instructionCounter = 0; // The next instruction to parse.
        private Rigidbody2D _rb2D;

        // Use this for initialization
        void Awake ()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per physics frame
        void FixedUpdate () {
            if (_instructionCounter >= Instructions.Count)
            {
                // We went through the list and was told to stort over. Go to first instruction.
                if (!RepeatInstructionsOnCompletion)
                {
                    this.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    _instructionCounter = 0;    
                }
            }

            // Parse the next instruction in the instruction list, if the instruction was not completed,
            // as if the destination of Move was not reached, just exit.
            var completedInstruction = ParseInstruction(Instructions[_instructionCounter]);
            if (!completedInstruction)
            {
                Debug.Log("Still processing instruction " + _instructionCounter);
                return;
            }

            Debug.Log("Incrementing counter");
            // Otherwise increase instruction counter.
            _instructionCounter++;
        }

        private bool ParseInstruction(Instruction instruction)
        {
            switch (instruction.GetType())
            {
                case InstructionType.Move:
                    return Move((MoveInstruction) instruction);

                case InstructionType.Shoot:
                    return Shoot((ShootInstruction) instruction);

                case InstructionType.MoveAndShoot:
                    return MoveShoot((MoveShootInstruction) instruction);

                case InstructionType.Idle:
                    return Idle((IdleInstruction) instruction);
            }
            return false;
        }

        private bool Idle(IdleInstruction instruction)
        {
            Debug.Log("Idle. Current instruction counter = " + this._instructionCounter);
            if (instruction.DoneAt < 0f)
            {
                instruction.DoneAt = Time.time + instruction.timeToIdle;
            }

            return Time.time >= instruction.DoneAt;
        }

        private bool MoveShoot(MoveShootInstruction instruction)
        {
            Debug.Log("MoveShoot. Current instruction counter = " + this._instructionCounter);
            var shoot = Shoot(instruction.shootInstruction);
            var move = Move(instruction.moveInstruction);
            return shoot && move;
        }

        private bool Shoot(ShootInstruction instruction)
        {
            Debug.Log("Shoot. Current instruction counter = " + this._instructionCounter);
            if (!_shooting)
            {
                _shooting = true;
                Weapons.Engage(instruction.weaponsToFire, instruction.volleys);
                return false;
            }
            else
            {
                _shooting = Weapons.gameObject.activeInHierarchy;
                return !_shooting;
            }
        }

        // Returns true if the instruction is completed (no more waypoints.)
        private bool Move(MoveInstruction instruction)
        {
            Debug.Log("Move instruction. Current instruction counter = " + this._instructionCounter);
            // Calculate distance to waypoint
            var wp = instruction.GetCurrentWaypoint();
            if (wp == null)
            {
                Debug.Log("Null waypoint returned for move!!");
                return true;
            }

            var sqrRemainingDistance = (_rb2D.position - wp.Position).sqrMagnitude;
            Vector2 newPostion;

            if (sqrRemainingDistance < float.Epsilon && instruction.HasNextWaypoint())
            {
                // If we're reasonably close and there is more waypoints, switch WP and move.
                instruction.SwitchWaypoint();
                wp = instruction.GetCurrentWaypoint();
                newPostion = Vector3.MoveTowards(_rb2D.position, wp.Position, wp.Speed * Time.deltaTime);
                _rb2D.MovePosition(newPostion);
                return false;
            }
            if (sqrRemainingDistance < float.Epsilon && !instruction.HasNextWaypoint())
            {
                // If we're close but there is no new WP, simply return, we'll move next tick.
                instruction.SwitchWaypoint(); // Just roll it over if we need to revisit.
                return true;
            }

            // We're not close, lets move!
            newPostion = Vector3.MoveTowards(_rb2D.position, wp.Position, wp.Speed * Time.deltaTime);
            _rb2D.MovePosition(newPostion);
            return false;
        }
    }
}