using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
