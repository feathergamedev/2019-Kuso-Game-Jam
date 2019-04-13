using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    [SerializeField]
    Transform bg01;

    [SerializeField]
    Transform bg02;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bg01.position -= new Vector3(0.1f,0,0);
        if(bg01.position.x <= -19.5){
            bg01.position = new Vector3(19.5f,0,0);
        }
        bg02.position -= new Vector3(0.1f,0,0);
        if(bg02.position.x <= -19.5){
            bg02.position = new Vector3(19.5f,0,0);
        }
    }
}
