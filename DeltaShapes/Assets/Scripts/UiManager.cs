using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiManager : MonoBehaviour {
   
   

   
    public void StartButton ()
    {
        SceneManager.LoadScene(2);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    public void HelpButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Level1 ()
    {
        SceneManager.LoadScene(3);
    }

    public void Level2()
    {
        SceneManager.LoadScene(4);
    }

    public void Level3()
    {
        SceneManager.LoadScene(5);
    }

   

}
