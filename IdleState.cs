using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {

    public float startTime;
    public float idleTime;

    public IdleState(StateController stateController) : base(stateController)
    {
    }

    public void Start()
    {
        
    }

    public override void Act()
    {
        if (stateController.anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && !stateController.targetSet)
        {
            stateController.SetNewTarget("None");
        }
    }

    public override void OnStateEnter()
    {
        startTime = Time.time;
        idleTime = UnityEngine.Random.Range(1, 20);
        stateController.aSource.clip = stateController.screamSound;
        stateController.aSource.Play();
        stateController.anim.SetBool("Walk", false);
        
    }

    public override void OnStateExit()
    {
        stateController.targetSet = false;
    }

    public override void CheckTransitions()
    {
        if (stateController.inChaseRange())
        {
            stateController.SetState(new ChaseState(stateController));
        }

        if (!stateController.inChaseRange() && Time.time > startTime + idleTime)
        {
            stateController.SetState(new PatrolState(stateController));
        }

        if (stateController.inAttackRange())
        {
            stateController.SetState(new AttackState(stateController));
        }

    }

}
