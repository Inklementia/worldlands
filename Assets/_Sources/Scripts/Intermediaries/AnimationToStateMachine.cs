using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Enemies.States;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState AttackState;

    private void TriggerAttack()
    {
        AttackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        AttackState.FinishAttack();
    }
}
