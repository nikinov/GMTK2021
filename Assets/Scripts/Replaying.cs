using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replaying : MonoBehaviour
{

    /// <summary>
    /// replaying and exit functions for the buttons
    /// </summary>
    public void replay()
    {

        SceneManager.LoadScene("sc1");

    }

    public void Exit()
    {

        Application.Quit();

    }

}
