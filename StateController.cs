using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {


    public State currentState;
    private GameObject[] navPoints;
    private GameObject[] hidePoints;
    public GameObject enemyToChase;
   
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    
    public GameObject[] enemies;
    public float detectionRange = 7;
    public float attackRange = 0.5f;
    public double health = 100.0;
    public bool targetSet = false;

    [HideInInspector]
    public Animator anim;
    public AudioClip screamSound;
    public AudioClip attackSound;
    public AudioClip walkingSound;
    [HideInInspector]
    public AudioSource aSource;
    
    // Use this for initialization
    void Start () {
        aSource = GetComponent<AudioSource>();
        enemyToChase = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        navPoints = GameObject.FindGameObjectsWithTag("Patrol");
        SetState(new IdleState(this));

    }
	
	// Update is called once per frame
	void Update () {
        currentState.Act();
        currentState.CheckTransitions();
        CheckAudio();

    }

    //checking and changing audio volume based on distance to the player
    private void CheckAudio()
    {

        aSource.volume = 1 / Vector3.Distance(enemyToChase.transform.position, this.gameObject.transform.position);


    }

    //checking if zombie is close enogh to chase but not close enough to attack
    public bool inChaseRange()
    {
        enemies = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in enemies)
        {
            if(Vector3.Distance(g.transform.position, transform.position) < detectionRange && Vector3.Distance(g.transform.position, transform.position) > attackRange)
            {
                return true;
            }
        }

        return false;
    }

    //checking if zombie can attack
    public bool inAttackRange()
    {
        enemies = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in enemies)
        {
            if (Vector3.Distance(g.transform.position, transform.position) < attackRange )
            {
                return true;
            }
        }

        return false;
    }

    //changing state
    public void SetState(State state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "Zombie agent in state " + state.GetType().Name;


        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    //setting object to target
    public void SetNewTarget(string thingToTarget)
    {
        Transform target = this.transform;
        if (thingToTarget == "Player")
        {
            target = enemyToChase.transform;
            
        }

        else if(thingToTarget == "Patrol"){
            int i = UnityEngine.Random.Range(0, navPoints.Length);
            target = navPoints[i].transform;
            targetSet = true;
        }else if(thingToTarget == "None")
        {
            target = this.transform;
            targetSet = true;
        }

        ai.SetTarget(target);
        
        

    }

    //take damage and go to death state
    public void TakeDamage()
    {
        SetState(new DeathState(this));
    }

    public IEnumerator ChangeStateTimer(State nextState)
    {
        yield return new WaitForSeconds(1);
        SetState(nextState);

    }
}
