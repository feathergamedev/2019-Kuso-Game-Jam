using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabs = new GameObject[1];

    public int randMin = 0;
    public int randMax = 0;

    public float sec = 0f;
    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawn(){
        while(true){
            if(prefabs != null && randMax <= prefabs.Length -1){
                rand = Random.Range(randMin,randMax);
                Instantiate(prefabs[rand]);

                yield return new WaitForSeconds(sec);
            }

            yield return null;
        }
    }
}
