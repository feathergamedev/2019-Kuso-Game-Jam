using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject deathMenu;

    [SerializeField]
    private GameObject completeMenu;

    [SerializeField]
    private GameObject gameUI;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart(){
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
    }
    public void GamePause(){
        pauseMenu.SetActive(true);
        gameUI.SetActive(false);
    }
    public void UnPause(){
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
    }
    public void HarderLevel(){
        //HARRRRDERRRRRRR
    }

    public void PlayAgain(){
        //reload?
    }

    public void Fail(){
        deathMenu.SetActive(true);
    }


}
