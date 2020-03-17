using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public float startTime;
    public float patrolTime;

    public PatrolState(StateController stateController) : base(stateController)
    {
    }

    public void Start()
    {
        
    }

    public override void Act()
    {
        if (stateController.anim.GetCurrentAnimatorStateInfo(0).IsName("walk_in_place"))
        {
            if (stateController.ai.destinationReached || !stateController.targetSet)
            {
                stateController.SetNewTarget("Patrol");
            }
        }
        
    }

    public override void OnStateEnter()
    {
        //stateController.anim.Play("walk_in_place");
        stateController.anim.SetBool("Walk", true);
        Debug.Log("Entering Patrol");
        startTime = Time.time;
        stateController.aSource.clip = stateController.walkingSound;
        stateController.aSource.Play();
        patrolTime = UnityEngine.Random.Range(1, 120);
        
    }

    public override void OnStateExit()
    {
        stateController.targetSet = false;
        Debug.Log("Exiting Patrol");
    }

    public override void CheckTransitions()
    {
        if (stateController.inChaseRange())
        {
            stateController.SetState(new ChaseState(stateController));
        }

        if(!stateController.inChaseRange() && Time.time > startTime + patrolTime)
        {
            stateController.SetState(new IdleState(stateController));
        }

        if (stateController.inAttackRange())
        {
            stateController.SetState(new AttackState(stateController));
        }

    }

}