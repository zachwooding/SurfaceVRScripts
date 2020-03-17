using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLamp : MonoBehaviour {
    public GameObject redLamp;
    public GameObject spinner;

    int x = 0;
    private float RotateSpeed = 5f;
    private float Radius = 0.2f;

    private Vector3 _centre;
    private float _angle;
    
    // Use this for initialization
    void Start () {
        _centre = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle)) * Radius;
        spinner.transform.position = _centre + offset;
        spinner.transform.LookAt(_centre);
	}
}
