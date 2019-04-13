using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    [SerializeField]
    float spawnTime = 2.0f;

    float time;

    void Start()
    {
        time = 0;
        if (Enemy == null) Enemy = GameObject.Find("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > spawnTime)
        {
            Spawn();
            time = 0;
        }
    }

    void Spawn()
    {
        Instantiate(Enemy, transform.position + new Vector3(Random.Range(-2,2),1,0), Quaternion.identity);
    }
}
