using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum MoveType
{
    MoveX,
    Jump,
    Shadow
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
    [Header("忍者分身時間與距離")]
    [SerializeField]
    float splitTime = 2.0f;
    public float min = -2 , max = 2;
    private Rigidbody m_Rigidbody; 
    [SerializeField]
    private float m_MovementInputValue;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the enemy is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        //m_MovementInputValue = 0.03f;
    }


    private void OnDisable()
    {
        // When the enemy is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        //DOJump();
        if(m_type == MoveType.Shadow)
        Invoke("ShadowSplit", splitTime);
    }

    public void MovementInput(float forwardBackward)
    {
        m_MovementInputValue = forwardBackward;
    }

    void DOJump()
    {
        m_Rigidbody.DOJump(transform.position + Vector3.left * jumpValue, 2, 1, jumpTime);
    }

    void ShadowSplit()
    {
        //gameObject.AddComponent<EnemySpawn>();
        GameObject Enemy = gameObject;
        var vx = Random.Range(min, max);
        while(vx == 0) { vx = Random.Range(min, max); }
        Instantiate(Enemy, transform.position + new Vector3(Random.Range(min, max), 1, 0), Quaternion.identity);
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
    }

    private void Move()
    {
        Vector3 movement = Vector3.left * m_MovementInputValue * Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

}
