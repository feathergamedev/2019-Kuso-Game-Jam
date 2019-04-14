using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum LevelState { Playing, Pause, Fail, NextLevel, WatchAd} 

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    public LevelState m_levelState;

    [Header("目前的關卡等級")]
    public int cur_level;


    [Header("每一關的持續時間")]
    [SerializeField]
    private float duration_per_level;

    private float level_time;

    [SerializeField]
    Text Text_LevelInfo;

    [SerializeField]
    string prepared_words;

    public DeadBackgroundCtrl deadBackground;
    public Generator generator;

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

    public void Next_Level(){
        //HARRRRDERRRRRRR
    }

    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 由Enemy呼叫
    /// </summary>
    public void Fail(){
        if (m_levelState == LevelState.Fail)
            return;

        PlayerManager.instance.Die();
        SoundManager.instance.Play_Sound(SoundType.主角死亡);

        Invoke("Show_Die_Background", 0.3f);        

        m_levelState = LevelState.Fail;
    }

    void Show_Die_Background()
    {
        deadBackground.Play();
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

        if(m_levelState != LevelState.Fail)
            m_levelState = LevelState.Playing;

    }

    void Show_Ads()
    {
        Fake_AdCounter.instance.Start_CountDown();
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

                    if (Input.GetButtonDown("Charge"))
                    {
                        Show_Ads();
                        deadBackground.Hide_Anim();
                        m_levelState = LevelState.WatchAd;
                    }


                    break;
                case LevelState.NextLevel:

                    prepared_words = string.Format("Level {0}", cur_level + 1);

                    StartCoroutine(LevelText_Performance());

                    //Show "第幾關" UI
                    yield return new WaitForSeconds(4.0f);

                    m_levelState = LevelState.Playing;

                    switch(cur_level)
                    {
                        case 1:
                            generator.randMin = 0;
                            generator.randMax = 1;


                            break;
                        case 2:
                            generator.randMin = 1;
                            generator.randMax = 3;
                            break;
                        case 3:
                            generator.randMin = 1;
                            generator.randMax = 3;
                            break;
                    }

                    if(generator.born_coolDown > 0.3f)
                        generator.born_coolDown -= 0.2f;

                    break;
                case LevelState.WatchAd:

                    // 5秒後才能關掉廣告 (另外加一個UI準備秀出的時間);
                    yield return new WaitForSeconds(6.0f);

                    while(true)
                    {
                        if (Input.GetButtonDown("Charge"))
                        {
                            yield return null;
                            Replay();

                        }
                        else
                            yield return null;
                    }
            }

            yield return null;
        }
    }

}
