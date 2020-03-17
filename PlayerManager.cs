using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Zach Wooding
 * 
 * Manages player health and blood overlay
 */


public class PlayerManager : MonoBehaviour {

    public float health = 3;
    public GameObject overlay1;
    public GameObject overlay2;
    public GameObject overlay3;
    LevelManager lvlManager;
	// Use this for initialization
	void Start () {
        lvlManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
    
	void Update () {

        UpdateHealthOverlay();
    }

    //checks and updates blood overlay
    public void UpdateHealthOverlay()
    {
        if (health <= 0)
        {
            lvlManager.gameOver();
            overlay1.SetActive(false);
            overlay2.SetActive(false);
            overlay3.SetActive(true);
        }

        if (health == 3)
        {
            overlay1.SetActive(false);
            overlay2.SetActive(false);
            overlay3.SetActive(false);
        }

        if (health == 2)
        {
            overlay1.SetActive(true);
            overlay2.SetActive(false);
            overlay3.SetActive(false);
        }

        if (health == 1)
        {
            overlay1.SetActive(false);
            overlay2.SetActive(true);
            overlay3.SetActive(false);
        }
    }

    //called when the enemy strikes the player
    public void TakeDamage()
    {
        health--;
        StartCoroutine(Heal());
    }

    //heal after 10 seconds
    IEnumerator Heal()
    {
        yield return new WaitForSeconds(10);
        if(health < 3)
        {
            health++;
        }
    }
}
