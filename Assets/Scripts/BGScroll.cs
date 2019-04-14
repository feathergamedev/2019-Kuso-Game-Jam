using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    [SerializeField]
    Transform bg01;

    [SerializeField]
    Transform bg02;

    [SerializeField]
    private float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bg01.localPosition -= new Vector3(scrollSpeed, 0,0);
        if(bg01.localPosition.x <= -3.31f){
            bg01.localPosition = new Vector3(4.30f,0,0);
        }

        bg02.localPosition -= new Vector3(scrollSpeed, 0,0);
        if(bg02.localPosition.x <= -3.31f){
            bg02.localPosition = new Vector3(4.30f,0,0);
        }
    }
}
