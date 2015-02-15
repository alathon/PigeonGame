using UnityEngine;
using System.Collections;
using Assets.Scripts.Enemy;

public class IdleInstruction : Instruction
{
    public float timeToIdle = 1f;
    [HideInInspector] public float DoneAt = -1f;

    public override InstructionType GetType()
    {
        return InstructionType.Idle;
    }
}
