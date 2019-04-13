using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState { Playing, Pause, Fail, NextLevel} 

public class LevelManager : MonoBehaviour
{

    public LevelState m_levelState;

    [Header("目前的關卡等級")]
    public int cur_level;

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

    [Header("每一關的持續時間")]
    [SerializeField]
    private float duration_per_level;

    private float level_time;






    // Start is called before the first frame update
    void Start()
    {
        level_time = 0.0f;
        m_levelState = LevelState.Playing;

        StartCoroutine(Level_Machine());
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
    public void Next_Level(){
        //HARRRRDERRRRRRR
    }

    public void Replay(){
        //reload?
    }

    public void Fail(){
        deathMenu.SetActive(true);
    }

    IEnumerator Level_Machine()
    {
        yield return null;

        while(true)
        {
            switch(m_levelState)
            {
                case LevelState.Playing:

                    level_time += Time.fixedDeltaTime;

                    Debug.Log(level_time);

                    if (level_time >= duration_per_level)
                    {
                        cur_level++;
                        level_time = 0.0f;

                        m_levelState = LevelState.NextLevel;
                    }


                    break;
                case LevelState.Pause:
                    break;
                case LevelState.Fail:
                    break;
                case LevelState.NextLevel:

                    Debug.LogFormat("進入Level {0}", cur_level);

                    //Show "第幾關" UI
                    yield return new WaitForSeconds(3.0f);
                    m_levelState = LevelState.Playing;

                    break;
            }
        }
    }


}
