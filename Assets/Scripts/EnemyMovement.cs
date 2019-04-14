using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum MoveType
{
    MoveX,
    Jump,
    Shadow,
    Drunk
}

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    MoveType m_type;
    [Header("直走怪跑速")]
    public float Speed = 12f;

    [Header("跳跳怪跳躍時間")]
    [SerializeField]
    float jumpTime = 2.0f;
    [SerializeField]
    float jumpValue = 2.0f;
    float time;

    [SerializeField]
    public Vector3 jumpForce;

    [Header("忍者分身時間與距離")]
    [SerializeField]
    float splitTime = 2.0f;
    public float min = -2 , max = 2;

    [Header("酒醉位移")]
    public float DrunkX = -2;
    public float DrunkXm = 0.6f;

    private Rigidbody2D m_Rigidbody; 
    [SerializeField]
    private float m_MovementInputValue;
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        //DOJump();
        if(m_type == MoveType.Shadow)
        Invoke("ShadowSplit", splitTime);

        Speed *= UnityEngine.Random.Range(1.0f, 1.7f);

        Speed += LevelManager.instance.cur_level * 2f;


        jumpValue += LevelManager.instance.cur_level * 1.2f;
        DrunkX -= LevelManager.instance.cur_level * 0.6f;
        DrunkXm += LevelManager.instance.cur_level * 0.2f;
    }

    public void MovementInput(float forwardBackward)
    {
        m_MovementInputValue = forwardBackward;
    }

    void DOJump()
    {
        //        transform.DOJump(transform.position + Vector3.left * jumpValue, 2, 1, jumpTime);
        GetComponent<Rigidbody2D>().AddForce(jumpForce);
    }

    void ShadowSplit()
    {
        //gameObject.AddComponent<EnemySpawn>();
        GameObject Enemy = gameObject;        
        Instantiate(Enemy, transform.position + new Vector3(UnityEngine.Random.Range(min, max), 0, 0), Quaternion.identity);
    }

    private void Update()
    {
        if(m_type == MoveType.Jump)
        {
            time += Time.deltaTime;
            if (time > jumpTime)
            {
                DOJump();
                time = 0;
            }
        }        


        
    }
    private void FixedUpdate()
    {
        if (m_type == MoveType.MoveX || m_type == MoveType.Shadow)
        {
            Move();
        }
        if(m_type == MoveType.Drunk)
        {
            time += Time.fixedDeltaTime;
            if (time > 1)
            {
                DrunkMove();
                time = 0;
            }
        }
    }

    private void DrunkMove()
    {
        m_Rigidbody.DOMoveX(transform.position.x + DrunkX, 0.66f).SetEase(Ease.InBounce).OnComplete(()=> { m_Rigidbody.DOMoveX(transform.position.x + DrunkXm, 0.33f).SetEase(Ease.OutBounce).onComplete(); });
        
        //m_Rigidbody.DOMoveX(-0.5f, 1).SetEase(Ease.InCirc);
    }

    private void Move()
    {
        Vector2 movement = Vector3.left * m_MovementInputValue * Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

}
