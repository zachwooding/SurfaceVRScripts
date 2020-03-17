using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {
    public GameObject player;
    private float speed = 0.1f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z +1*speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            player.transform.position = new Vector3(player.transform.position.x + 1 * speed, player.transform.position.y, player.transform.position.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 1 * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position = new Vector3(player.transform.position.x - 1 * speed, player.transform.position.y, player.transform.position.z );
        }
    }
}
