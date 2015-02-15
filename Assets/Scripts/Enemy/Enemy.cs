﻿using System;
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

            if (_instructionCounter >= Instructions.Count && RepeatInstructionsOnCompletion)
            {
                // We went through the list and was told to stort over. Go to first instruction.
                _instructionCounter = 0;
            }
            else if (_instructionCounter >= Instructions.Count && !RepeatInstructionsOnCompletion)
            {
                // We went through the list but should not start over. Stay still.
                return;
            }

            // Parse the next instruction in the instruction list, if the instruction was not completed,
            // as if the destination of Move was not reached, just exit.
            var completedInstruction = ParseInstruction(Instructions[_instructionCounter]);
            if (!completedInstruction) return;

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
            }
            return false;
        }

        private bool MoveShoot(MoveShootInstruction instruction)
        {
            var shoot = Shoot(instruction.shootInstruction);
            var move = Move(instruction.moveInstruction);
            return shoot && move;
        }

        private bool Shoot(ShootInstruction instruction)
        {
            if (!_shooting)
            {
                Weapons.Engage(instruction.weaponsToFire, instruction.volleys);
                _shooting = true;
                return false;
            }
            else
            {
                return Weapons.gameObject.activeInHierarchy;
            }
        }

        // Returns true if the instruction is completed (no more waypoints.)
        private bool Move(MoveInstruction instruction)
        {
            // Calculate distance to waypoint
            var wp = instruction.GetCurrentWaypoint();
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