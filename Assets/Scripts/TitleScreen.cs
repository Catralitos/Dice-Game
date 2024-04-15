using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void RestartButton(){
        SceneManager.LoadScene("ShopScene");
    }
    public void CreditsButton(){
        SceneManager.LoadScene("CreditsScreen");
    }
    public void ExitButton(){
        Application.Quit();
        Debug.Log("Exit button clicked in editor.");
    }
}
