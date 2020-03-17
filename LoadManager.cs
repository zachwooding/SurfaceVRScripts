using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

    // Use this for initialization
    public Scene game;
    public AudioSource source;
    private int diff = 1;
    public GameObject easy, medium, hard;
    public Material onMat, offMat;
    public GameObject fadeScreen;

	void Start () {
        source.Play();
	}
	
	// Update is called once per frame
	void Update () {

        if(easy == null)
        {
            return;
        }


        //setting the visuals based on selected difficulty
        if(diff == 1)
        {
            easy.GetComponent<MeshRenderer>().material = onMat;
        }else
        {
            easy.GetComponent<MeshRenderer>().material = offMat;
        }

        if (diff == 2)
        {
            medium.GetComponent<MeshRenderer>().material = onMat;
        }
        else
        {
            medium.GetComponent<MeshRenderer>().material = offMat;
        }

        if (diff == 3)
        {
            hard.GetComponent<MeshRenderer>().material = onMat;
        }
        else
        {
            hard.GetComponent<MeshRenderer>().material = offMat;
        }
    }

    //setting difficulty in game settings
    public void difSet(int x)
    {
        
        diff = x;
        GameSettings.Difficulty = diff;
    }

    //loading scenes based on hit
    public void loadScene2() 
    {
        StartCoroutine(ChangeSceneTimer("SubRoom2"));
    }

    public void loadMainMenu()
    {
        StartCoroutine(ChangeSceneTimer("MainMenu"));
    }

    public void loadInsrutions()
    {
        StartCoroutine(ChangeSceneTimer("Instructions"));
        
    }
    public void quit()
    {
        Application.Quit();
    }

    //end level after seconds
    IEnumerator ChangeSceneTimer(string sceneName)
    {
        fadeScreen.GetComponent<Animator>().SetBool("isFading", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }

}
