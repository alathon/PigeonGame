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
        public Instruction[] Instructions;                 // List of Instructions the AI must carry out.
        public bool RepeatInstructionsOnCompletion = true; // Should the enemy reåeat the instruction list if it completes all?

        // Weapon related stuff.
        public WeaponSystem Weapons; // List of weapons the enemy carries.

        // Use this for initialization
        void Start () {

        }
	
        // Update is called once per physics frame
        void FixedUpdate () {
	
        }
    }
}