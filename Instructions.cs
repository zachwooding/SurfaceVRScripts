using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rotates and adjusts the instructions so the player can always see it
public class Instructions : MonoBehaviour {

    public Transform playerRot;
    public Transform instruct;
    // Use this for initialization
    void Start () {
        instruct = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        instruct.SetPositionAndRotation(new Vector3(playerRot.position.x + 0.5f, playerRot.position.y - 0.6f, playerRot.position.z + 0.3f), playerRot.rotation);
    }
}
