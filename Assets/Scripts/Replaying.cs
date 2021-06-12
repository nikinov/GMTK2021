using UnityEngine;
using UnityEngine.SceneManagement;

public class Replaying : MonoBehaviour
{
    // Replaying and exit functions for the buttons
    public void Replay()
    {
        SceneManager.LoadScene("sc1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}