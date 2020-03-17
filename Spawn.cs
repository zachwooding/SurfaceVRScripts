using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject enemyToSpawn;
    public GameObject[] spawnPoints;

    private GameObject[] enemysAlive;
    private GameObject player;
    private List<GameObject> lowPoint = new List<GameObject>();
    private List<GameObject> highPoint = new List<GameObject>();
    public GameObject enemyContainer;
    LevelManager lvlMang;
	// Use this for initialization
    //Game is started, spawn points determined
	void Start () {
        lvlMang = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        player = GameObject.FindGameObjectWithTag("Player");
        foreach(GameObject s in spawnPoints)
        {
            if(s.transform.position.y > -1.5f)
            {
                highPoint.Add(s);
            }
            else
            {
                lowPoint.Add(s);
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        enemysAlive = GameObject.FindGameObjectsWithTag("Enemy");

        //spawn enemies if there are less than the difficulty number
        if(enemysAlive.Length < lvlMang.difficulty)
        {
            //spawning and sestting location
            GameObject newEnemy = Instantiate(enemyToSpawn, enemyContainer.transform);
            if (player.transform.position.y < -1.5f)
            {
                newEnemy.transform.position = highPoint[Random.Range(0, highPoint.Count - 1)].transform.position;
            }
            else
            {
                newEnemy.transform.position = lowPoint[Random.Range(0, lowPoint.Count - 1)].transform.position;
            }
            
        }
	}

    //enemy takes damage called to start the despawn timer
    public void DestroyEnemy(GameObject enemyToDestory)
    {
        StartCoroutine(DespawnEnemyTimer(enemyToDestory));
        
    }

    //destroys object after timer ends
    public IEnumerator DespawnEnemyTimer(GameObject destroyObj)
    {
        yield return new WaitForSeconds(2);
        Destroy(destroyObj);
    }
    
}
