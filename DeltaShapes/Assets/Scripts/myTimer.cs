using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class myTimer : MonoBehaviour {

    public static float myCooltimer=5;
    public Text timerText;
   
    public GameObject GameManager;
    void Awake()
    {
       
        myCooltimer = 60;
    }
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        myCooltimer -= Time.deltaTime;
        timerText.text = myCooltimer.ToString("f0");
        if (myCooltimer <= 0)
        {

           PlayerPrefs.SetFloat("score" , GameManager.GetComponent<GridManager>().score );
             if (PlayerPrefs.GetFloat("highScore") < PlayerPrefs.GetFloat("score"))
                PlayerPrefs.SetFloat("highScore", PlayerPrefs.GetFloat("score")); 
            Time.timeScale = 0;
			PlayerPrefs.Save ();
            SceneManager.LoadScene("GameOver");


        }
        
    }
}
