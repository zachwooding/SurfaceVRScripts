using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//flicker flash light on and off
public class LightFlicker : MonoBehaviour {
    public List<GameObject> lights;
    private float flickerTime;
    private float lastFlicker;
    private float startFlicker;
    private bool lightsOff;
    private float offTime;
    public AudioClip switchAudio;
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        flickerTime = Random.Range(5f, 30f);
        lastFlicker = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(lastFlicker + flickerTime < Time.time && !lightsOff)
        {
            
            print("Off");
            foreach(GameObject g in lights)
            {
                g.SetActive(false);
            }
            source.Play();
            startFlicker = Time.time;
            offTime = Random.Range(0.1f, 4f);
            lightsOff = true;
        }

        if(startFlicker + offTime < Time.time && lightsOff)
        {
            print("On");
            foreach (GameObject g in lights)
            {
                g.SetActive(true);
            }
            source.Play();
            flickerTime = Random.Range(5f, 30f);
            lastFlicker = Time.time;
            lightsOff = false;
        }
	}
}
