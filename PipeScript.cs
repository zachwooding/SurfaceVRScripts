using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PipeScript : MonoBehaviour {

    private Animator anim;
    // Use this for initialization
    private UnityAction openListener;
   private UnityAction closeListener;
    public UnityEvent valveOpen;
    public UnityEvent valveClosed;
    public bool isOpen;

    public Light light;

    private void Awake()
    {
        openListener = new UnityAction(openPipe);
        closeListener = new UnityAction(closePipe);
    }
    void Start () {
        anim = GetComponent<Animator>();
        isOpen = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openPipe()
    {
        Debug.Log("Open");
        anim.SetBool("isOpen", true);
        light.color = Color.green;
        valveOpen.Invoke();
        isOpen = true;
    }

    public void closePipe()
    {
        Debug.Log("Closed");
        //anim.Play("Pipe");
        anim.SetBool("isOpen", false);
        light.color = Color.red;
        valveClosed.Invoke();
        isOpen = false;
    }
}
