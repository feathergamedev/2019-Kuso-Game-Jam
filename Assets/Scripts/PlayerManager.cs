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

    [Header("道具拋出力道")]
    [SerializeField]
    private float power;

    [Header("標示目前攻擊位置的Bar條")]
    [SerializeField]
    private GameObject attackPos_bar;

    Weapon m_curWeapon;

    private float cur_charge;

    [Header("發射狀態會持續多久")]
    [SerializeField]
    private float fire_coolDown;

    [Header("武器落地的Y座標")]
    [SerializeField]
    private float floorPos_Y;


    Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        if (power <=0 )
        {
            power = 5;
        }
        Init();
        Set_AttackPos_Bar();

        StartCoroutine(State_Machine());

        m_animator = GetComponent<Animator>();
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
                    cur_charge += 33f * Time.deltaTime;

                    if (Input.GetButtonUp("Charge"))
                        playerState = PlayerState.Fire;


                    attackPos_bar.transform.position = new Vector3( (cur_charge * charge_speed) + transform.position.x, 0.0f, 0.0f);

                    break;
                case PlayerState.Fire:

                    attackPos_bar.GetComponent<SpriteRenderer>().enabled = false;
                    StartCoroutine(Fire(attackPos_bar.transform.position.x - transform.position.x , attackPos_bar.transform));



                    yield return new WaitForSeconds(fire_coolDown);

                    cur_charge = 0.0f;
                    attackPos_bar.GetComponent<SpriteRenderer>().enabled = false;
                    attackPos_bar.transform.position = new Vector3(transform.position.x, 0.0f, 0.0f);


                    playerState = PlayerState.Idle;
                    break;
            }
        }



    }
    
  
    IEnumerator Fire(float distance,Transform target)
    {
        Vector3 target_pos = transform.position + new Vector3(distance, transform.position.y, 0.0f);

        m_curWeapon = WeaponManager.instance.cur_weapon;

        m_animator.SetTrigger("Shoot");

        //根據武器類型來加音效

        float m_born_posY = m_curWeapon.GetComponent<Weapon>().born_posY;

        GameObject attack = Instantiate(m_curWeapon.gameObject, new Vector3(-7.94f, m_born_posY,0f), transform.rotation);
        
        float y = attack.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        print(attack.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        attack.transform.DOJump(new Vector3(target.position.x, -5 + y, 0), power, 1, 0.5f, false);
        // Move curve
        /*
        attack.transform.DOMoveX(transform.position.x + distance/4, 0.2f).SetEase(Ease.InCirc);
        attack.transform.DOMoveY(transform.position.y + 5.5f, 0.2f).SetEase(Ease.InCirc);
        yield return new WaitForSeconds(0.2f);
        attack.transform.DOMoveY(floorPos_Y, 0.3f).SetEase(Ease.InCirc);
        attack.transform.DOMoveX(transform.position.x + distance, 0.3f).SetEase(Ease.InCirc);

        */
        yield return new WaitForSeconds(0.5f);

        CameraManager.instance.Shake();

        Set_AttackPos_Bar();

        Destroy(attack, 0.45f);
    }

    void Set_AttackPos_Bar()
    {
        WeaponManager.instance.Switch_To_Next_Weapon();
        m_curWeapon = WeaponManager.instance.cur_weapon;
        attackPos_bar.transform.localScale = new Vector3(m_curWeapon.transform.localScale.x, 10.8f, 1.0f);
    }

    void Init()
    {
        playerState = PlayerState.Idle;

        attackPos_bar.GetComponent<SpriteRenderer>().enabled = false;
        attackPos_bar.transform.position = transform.position;
    }


}
