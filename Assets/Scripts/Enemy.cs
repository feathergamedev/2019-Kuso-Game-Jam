using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int HP;
    public float born_posY;

    public float particle_posY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        GameObject particle = PlayerManager.instance.Enemy_Particle;

        Instantiate(particle, transform.position + new Vector3(0.0f, particle_posY, 0.0f), transform.rotation);
        Debug.Log("Yo!");

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Weapon")
        {
            HP--;

            if(HP<=0)
            {
                Die();
            }

        }
        else if(collision.gameObject.tag=="Player")
        {
            LevelManager.instance.Fail();
        }
    }
}
