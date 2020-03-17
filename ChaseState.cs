using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//state that will tell the AI to follow the player when in range
public class ChaseState : State {
    public ChaseState(StateController stateController) : base(stateController)
    {
    }

    //setting the target and moving towards
    public override void Act()
    {
        if (stateController.anim.GetCurrentAnimatorStateInfo(0).IsName("walk_in_place") && !stateController.targetSet)
        {
            stateController.SetNewTarget("Player");
        }
    }

    //check transitions off state
    public override void CheckTransitions()
    {
        if (!stateController.inChaseRange())
        {
            stateController.SetState(new IdleState(stateController));
        }

        if (stateController.inAttackRange() && !stateController.inChaseRange())
        {
            stateController.SetState(new AttackState(stateController));
        }
    }

    public override void OnStateEnter()
    {
        stateController.anim.SetBool("Walk", true);
        stateController.anim.Play("walk_in_place");
        stateController.aSource.clip = stateController.walkingSound;
        stateController.aSource.Play();
        stateController.SetNewTarget("Player");
        Debug.Log("Entering Chase");

    }

    public override void OnStateExit()
    {
        stateController.anim.SetBool("Walk", false);
        stateController.targetSet = false;
        Debug.Log("Exiting Chase");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
