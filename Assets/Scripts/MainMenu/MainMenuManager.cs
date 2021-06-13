using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{

    public GameObject plane;



    private void Start()
    {
        plane.SetActive(true);
    }



    public void play()
  {

        SceneManager.LoadScene("sc2");

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
