using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Enum to define instruction type, currently moving, shooting and move-while-shooting are supported.
 */
public enum InstructionType
{
    Move,
    Shoot,
    MoveAndShoot
}

/**
 * Parent class for all instructions, must define a type to allow later parsing to identify them.
 * These classes are meant as containers, and should preferable not contain executable code, just variables.
 */
public abstract class Instruction
{
   public new abstract InstructionType GetType();
}

/**
 * Implementation of the Move instruction.
 */
public class MoveInstruction : Instruction
{
    public Vector2 Destination;


    public override InstructionType GetType()
    {
        return InstructionType.Move;
    }
}

public class Enemy : MonoBehaviour
{

    public Vector2 Position;     // Enemies position on screen.
    public float BaseSpeed = 5f; // Enemies base movement speed.
    public WeaponSystem Weapons; // List of weapons the enemy carries.

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
