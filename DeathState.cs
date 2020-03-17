using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State {
    public DeathState(StateController stateController) : base(stateController)
    {
    }

    public override void Act()
    {
        
    }

    public override void CheckTransitions()
    {
        
    }

    public override void OnStateEnter()
    {
        stateController.SetNewTarget("None");
        stateController.gameObject.transform.LookAt(stateController.enemyToChase.transform);
        stateController.anim.Play("fallingback");
        stateController.anim.SetBool("Hit", true);
        GameObject.FindGameObjectWithTag("EnemyControl").GetComponent<Spawn>().DestroyEnemy(stateController.gameObject);
        

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
