using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Credits")
        {
            if (Input.GetButtonDown("Charge"))
                Go_To_Home();
        }
    }

    public void Go_To_Home()
    {
        SceneManager.LoadScene("Bogay");
    }

    public void Go_To_Level()
    {
        SceneManager.LoadScene("Feather");

    }

    public void Go_To_Credit()
    {
        SceneManager.LoadScene("Credits");
    }
}
