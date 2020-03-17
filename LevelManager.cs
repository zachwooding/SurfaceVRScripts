using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/**
 * Created by Zach Wooding
 * 
 * Singlton 
 * Manages win/loss conditions, difficulty settings, scene timers, valve status, and scene navigation
 * 
 * */
public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    public GameObject[] valves; //tracks valves in scene
    private bool[] valveStatus; //tracks the status of each value
    public AudioSource source; //source for audio
    public AudioSource source2;
    public int difficulty = 1; //default difficulty is 1 to prevent null difficulty value
    private float timeLeft; //tracking how much time is left to complete the level
    public Text timerText; // printing time left
    private float seconds, minutes; //time elasped
    public GameObject fadeScreen;

    /*
     * On Awake check for instance of the singlton
     * 
     * */
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
       
    }

    // Use this for initialization
    void Start () {
        //Getting game difficulty
        difficulty = GameSettings.Difficulty;
        print(difficulty);
        if(difficulty == 0) //assuring difficulty is set
        {
            difficulty = 1;
        }

        //starting audio
        source.Play();
        source2.Play();
        
        valveStatus = new bool[valves.Length];

        //setting the correct game timer
        if(difficulty == 1)
        {
            timeLeft = 300.0f;
        }else if (difficulty == 2)
        {
            timeLeft = 200.0f;
        }else if (difficulty == 3)
        {
            timeLeft = 100.0f;
        }

        for (int x = 0; x < valves.Length; x++)
        {
            PipeScript pipe = valves[x].GetComponent<PipeScript>();
            valveStatus[x] = pipe.isOpen;
            
        }

        fadeScreen.GetComponent<Animator>().SetBool("isFading", false);
        
		
	}
	
    


	// Update is called once per frame
	void Update () {

        //updating time left
        minutes = (int)(timeLeft / 60f);
        seconds = (int)(timeLeft % 60f);

        //display time
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        timeLeft -= Time.deltaTime;

        //checking end game
        if (timeLeft < 0)
        {
            gameOver();
        }
		if(valveStatus[0] == false && valveStatus[1] == false && valveStatus[2] == true && valveStatus[3] == false && valveStatus[4] == true && valveStatus[5] == false && valveStatus[6] == true && valveStatus[7] == false)
        {
            endLevel();
        }

        
	}

    //called when a value gets opened
    public void valveOpen(GameObject valve)
    {
        for(int i = 0; i < valves.Length; i++)
        {
            //checkingt we got the right valve
            if(valves[i] == valve)
            {
                valveStatus[i] = true;
            }
        }
    }

    //called when a valve is closed
    public void valveClosed(GameObject valve)
    {

        for (int i = 0; i < valves.Length; i++)
        {
            //checking we got the right valve
            if (valves[i] == valve)
            {
                valveStatus[i] = false;
            }
        }
    }

    //load a scene on a win
    void endLevel()
    {
        
        StartCoroutine(EndGameTimer("LevelPassed"));
    }

    //start timer to end level
    public void gameOver()
    {
       
        StartCoroutine(EndGameTimer("GameOver"));
        
        
    }

    //end level after seconds
    IEnumerator EndGameTimer(string sceneName)
    {
        fadeScreen.GetComponent<Animator>().SetBool("isFading", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
    }
}
