using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenSkript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) GoToMenu();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

   
}
