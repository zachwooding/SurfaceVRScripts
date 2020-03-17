using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this state is called when the enemy is in attack range. plays attack animaton and damages the playert.
public class AttackState : State {

    //creating state
    public AttackState(StateController stateController) : base(stateController)
    {
    }

    public override void Act()
    {
        //playing the attack animation
        stateController.gameObject.transform.LookAt(stateController.enemyToChase.transform);
        if (stateController.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            stateController.anim.SetBool("Attack", true);
        }
        else
        {
            stateController.anim.SetBool("Attack", false);
            if (!stateController.aSource.isPlaying)
            {
                stateController.aSource.Play();
                stateController.enemyToChase.GetComponent<PlayerManager>().TakeDamage();
            }
           
        }

    }

    //checking transition
    public override void CheckTransitions()
    {
        if (stateController.inChaseRange() && !stateController.inAttackRange())
        {
            stateController.SetState(new ChaseState(stateController));
        }

        if (!stateController.inChaseRange() && !stateController.inAttackRange())
        {
            stateController.StartCoroutine(stateController.ChangeStateTimer(new PatrolState(stateController)));
            
        }
    }

    //on enter
    public override void OnStateEnter()
    {
        stateController.anim.Play("attack");
        stateController.enemyToChase.GetComponent<PlayerManager>().TakeDamage();
        stateController.gameObject.transform.LookAt(stateController.enemyToChase.transform);
        stateController.aSource.clip = stateController.attackSound;
        stateController.aSource.Play();
        stateController.SetNewTarget("None");
        
    }

   
    //on exit stop animator
    public override void OnStateExit()
    {
        stateController.anim.SetBool("Attack", false);
    }
    
}
