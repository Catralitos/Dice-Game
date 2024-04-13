using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartButton(){
        SceneManager.LoadScene("DiceGame");
    }
    public void CreditsButton(){
        SceneManager.LoadScene("CreditsScreen");
    }
    public void ExitButton(){
        Application.Quit();
    }
}