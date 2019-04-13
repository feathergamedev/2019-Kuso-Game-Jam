using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelState { Playing, Pause, Fail, NextLevel} 

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


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

    [SerializeField]
    Text Text_LevelInfo;

    [SerializeField]
    string prepared_words;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        level_time = 0.0f;
        m_levelState = LevelState.NextLevel;

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

    /// <summary>
    /// 由Enemy呼叫
    /// </summary>
    public void Fail(){
        deathMenu.SetActive(true);
    }

    IEnumerator LevelText_Performance()
    {
        int word_idx=0;

        yield return new WaitForSeconds(1.5f);

        while (word_idx < prepared_words.Length)
        {
            Text_LevelInfo.text += prepared_words[word_idx++];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.5f);

        Text_LevelInfo.text = "";

        m_levelState = LevelState.Playing;

    }

    IEnumerator Level_Machine()
    {


        while(true)
        {
            switch(m_levelState)
            {
                case LevelState.Playing:

                    level_time += Time.fixedDeltaTime;

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

                    prepared_words = string.Format("Level {0}", cur_level + 1);

                    StartCoroutine(LevelText_Performance());

                    //Show "第幾關" UI
                    yield return new WaitForSeconds(4.0f);

                    m_levelState = LevelState.Playing;

                    break;
            }

            yield return null;
        }
    }


}
