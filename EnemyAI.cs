using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour {
    private GameObject player;
    private GameObject enemy;
    private int ranLoc = 100;
    public float enemySpeed;
    public float retreatSpeed;
    public float idleTime;
    public UnityEvent killPlayer;
    private int health;
    private const int maxHealth = 100;
    private bool attack;
    private bool atHide;
    private bool idle;
    private string lastStatus;
    public GameObject[] hidePos;
    public Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        idleTime = idleTime * 2.5f;
        enemy = gameObject;
        enemy.transform.position = hidePos[0].transform.position;
        attack = true;
        atHide = false;
        idle = false;
        health = maxHealth;
        
    }

    void Update()
    {
        Status();

        if (attack)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);
            transform.LookAt(player.transform);
            anim.SetBool("Walk", true);
            //Debug.Log("Attacking");
        }

        if(transform.position == player.transform.position)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Attack", true);
            killPlayer.Invoke();
        }

        
        if (ranLoc == 100)
        {
            ranLoc = (int) Random.Range(0f, hidePos.Length);
        }

        if (!attack && !atHide)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, hidePos[ranLoc].transform.position, retreatSpeed);
            transform.LookAt(hidePos[ranLoc].transform.position);
            for (int x = 0; x < hidePos.Length; x++)
            {
                if (enemy.transform.position == hidePos[x].transform.position)
                {
                    atHide = true;
                    idle = true;
                    anim.SetBool("Walk", false);
                    //Debug.Log("Idling");
                    transform.LookAt(player.transform);
                    StartCoroutine(StartIdleTimer());
                }
            }
            // Debug.Log("Fleeing");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
            
        }

        if (health <= 50)
        {
            attack = false;
            
        }

    }

    public void TakeDamage()
    {
        health -= 10;
       
    }

    public IEnumerator StartIdleTimer()
    {   
        yield return new WaitForSeconds(idleTime);
        idle = false;
        attack = true;
        atHide = false;
        health = maxHealth;
        ranLoc = 100;
    }

    void Status()
    {
        string status = "";
        if (idle)
        {
            status += "idle ";
        }

        if (atHide)
        {
            status += "hid ";
        }

        if (attack)
        {
            status += "attack ";
        }

        if (!attack)
        {
            status += "flee ";
        }
        if(lastStatus != status)
        {
            Debug.Log(status);
        }

        lastStatus = status;
        
    }
}
