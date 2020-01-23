using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class GameOver : MonoBehaviour {

    public float score;
    public float highscore;
    public Text scoreT;
    public Text highScoreT;
	// Use this for initialization
	void Start () {

        score = Mathf.Round (PlayerPrefs.GetFloat("score"));
        highscore = Mathf.Round ( PlayerPrefs.GetFloat("highScore"));

        scoreT.text = score.ToString();
        highScoreT.text = highscore.ToString(); 
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
