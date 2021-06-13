using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{

    public GameObject plane;
    public GameObject first;
    public GameObject sec;




    public void song()
    {

        PlayerPrefs.SetString("#", "JOURNEY");
        SceneManager.LoadScene("sc2");
    }
    public void song1()
    {

        PlayerPrefs.SetString("#", "LOFI");
        SceneManager.LoadScene("sc2");
    }
    public void song2()
    {

        PlayerPrefs.SetString("#", "GREEN");
        SceneManager.LoadScene("sc2");
    }

    private void Start()
    {
        plane.SetActive(true);
    }



    public void play()
  {

        first.SetActive(false);
        sec.SetActive(true);
    }



    public void quit()
    {


        Application.Quit();



    }



    public void credits()
    {


        SceneManager.LoadScene("credits");


    }

    public void ok()
    {

        plane.SetActive(false);

    }
}
