using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabs = new GameObject[1];

    public int randMin = 0;
    public int randMax = 0;

    public float born_coolDown = 0f;
    private int rand;

    List<Enemy> all_enemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
        all_enemies = new List<Enemy>();
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    IEnumerator spawn(){
        while(true){

            if(LevelManager.instance.m_levelState == LevelState.Playing)
            {

                if (prefabs != null && randMax <= prefabs.Length - 1)
                {
                    rand = Random.Range(randMin, randMax+1);

                    GameObject newEnemy = Instantiate(prefabs[rand]);
                    newEnemy.transform.position = new Vector3(12f, newEnemy.GetComponent<Enemy>().born_posY, 0f);

                    all_enemies.Add(newEnemy.GetComponent<Enemy>());

                    yield return new WaitForSeconds(born_coolDown);
                }


            }
            else
            {
                yield return null;
            }


        }
    }

    public void Clear_Enemies()
    {
        foreach (Enemy e in all_enemies)
        {
            if(e != null)   
                Destroy(e.gameObject);
        }


        all_enemies.Clear();
    }
}
