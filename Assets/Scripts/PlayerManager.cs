using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum PlayerState { Idle, Charge, Fire}

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    PlayerState playerState;

    [Header("蓄力每1 Percent增加多少Unit的距離")]
    [SerializeField]
    private float charge_speed;

    [Header("標示目前攻擊位置的Bar條")]
    [SerializeField]
    private GameObject attackPos_bar;


    private float cur_charge;

    [Header("發射狀態會持續多久")]
    [SerializeField]
    private float fire_duration;

    [Header("目前所使用的武器")]
    [SerializeField]
    private GameObject cur_weapon;

    [Header("武器落地的Y座標")]
    [SerializeField]
    private float floorPos_Y;


    // Start is called before the first frame update
    void Start()
    {
        Init();

        StartCoroutine(State_Machine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator State_Machine()
    {
        while(true)
        {
            yield return null;

            switch (playerState)
            {
                case PlayerState.Idle:
                    if (Input.GetButtonDown("Charge"))
                    {
                        attackPos_bar.GetComponent<SpriteRenderer>().enabled = true;
                        playerState = PlayerState.Charge;
                    }


                    break;
                case PlayerState.Charge:
                    cur_charge += 33f * Time.fixedDeltaTime;

                    if (Input.GetButtonUp("Charge"))
                        playerState = PlayerState.Fire;


                    attackPos_bar.transform.position = new Vector3( (cur_charge * charge_speed) + transform.position.x, 0.0f, 0.0f);

                    break;
                case PlayerState.Fire:

                    attackPos_bar.GetComponent<SpriteRenderer>().enabled = false;
                    StartCoroutine(Fire(attackPos_bar.transform.position.x - transform.position.x));

                    yield return new WaitForSeconds(fire_duration);

                    cur_charge = 0.0f;
                    attackPos_bar.GetComponent<SpriteRenderer>().enabled = false;
                    attackPos_bar.transform.position = new Vector3(transform.position.x, 0.0f, 0.0f);


                    playerState = PlayerState.Idle;
                    break;
            }
        }



    }


    IEnumerator Fire(float distance)
    {
        Vector3 target_pos = transform.position + new Vector3(distance, transform.position.y, 0.0f);

        GameObject attack = Instantiate(cur_weapon, transform.position, transform.rotation);


        attack.transform.DOMoveX(transform.position.x + distance/4, 0.2f).SetEase(Ease.InCirc);
        attack.transform.DOMoveY(transform.position.y + 5.5f, 0.2f).SetEase(Ease.InCirc);
        attack.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCirc);

        yield return new WaitForSeconds(0.2f);

        attack.transform.DOMoveY(floorPos_Y, 0.3f).SetEase(Ease.InCirc);
        attack.transform.DOMoveX(transform.position.x + distance, 0.3f).SetEase(Ease.InCirc);
        yield return new WaitForSeconds(0.3f);

        Destroy(attack, fire_duration);
    }

    void Init()
    {
        playerState = PlayerState.Idle;

        attackPos_bar.GetComponent<SpriteRenderer>().enabled = false;
        attackPos_bar.transform.position = transform.position;
    }


}
