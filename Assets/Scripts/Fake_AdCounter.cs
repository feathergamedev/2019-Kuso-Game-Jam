using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fake_AdCounter : MonoBehaviour
{
    public static Fake_AdCounter instance;


    [SerializeField]
    Image countDown_circle;

    [SerializeField]
    Text countDown_timeNumber;

    float m_countDown_timer;

    [SerializeField]
    List<GameObject> all_Ads;

    int cur_idx;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_countDown_timer = 5.0f;

        transform.localScale = new Vector3(1.0f, 0.0f, 1.0f);

        cur_idx = Random.Range(0, all_Ads.Count);

        for(int i=0; i<all_Ads.Count; i++)
        {
            if (i == cur_idx)
                all_Ads[i].SetActive(true);
            else
                all_Ads[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Switch_Ad_Content();
        }
    }

    public void Switch_Ad_Content()
    {
        cur_idx = (cur_idx + 1) % all_Ads.Count;

        for (int i = 0; i < all_Ads.Count; i++)
        {
            if (i == cur_idx)
                all_Ads[i].SetActive(true);
            else
                all_Ads[i].SetActive(false);
        }
    }

    public void Show_Ads()
    {
        float y_scale = 1.0f;
//        DOTween.To(() => y_scale, x => y_scale = x, 1.0f, 0.5f);
        transform.localScale = new Vector3(1.0f, y_scale, 1.0f);
    }

    public void Hide_Ads()
    {
        transform.localScale = new Vector3(1.0f, 0.0f, 1.0f);

        cur_idx++;

        for (int i = 0; i < all_Ads.Count; i++)
        {
            if (i == cur_idx)
                all_Ads[i].SetActive(true);
            else
                all_Ads[i].SetActive(false);
        }
    }

    public void Start_CountDown()
    {
        Show_Ads();
        StartCoroutine(Count_Down());
    }

    IEnumerator Count_Down()
    {
        countDown_timeNumber.text = "5";

        yield return new WaitForSeconds(1.0f);

        while (m_countDown_timer>0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            m_countDown_timer--;
            countDown_timeNumber.text = "" + m_countDown_timer;
        }

        countDown_timeNumber.text = "X";


        Debug.Log("Show Cancel Icon.");

        yield return null;
    }
}
