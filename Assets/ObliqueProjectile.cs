using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliqueProjectile : MonoBehaviour {

    public float v0;
    public float angle;
    public float g;

    public float y;
    public float x;
        
    public float t;

    private Rigidbody rb;
    private bool start;

	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            start = true;
        }

        if (start)
        { 
            t += Time.deltaTime;
            Vector3 horizontal = new Vector3(v0 * Mathf.Cos(angle* Mathf.Deg2Rad) * t, 0, 0);
            Vector3 verticle = new Vector3(0, (v0 * Mathf.Sin(angle* Mathf.Deg2Rad) * t) - (0.5f * g * t * t), 0);

            transform.position += horizontal;
            transform.position += verticle;

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("enter");
        if(other.transform.tag == "Ground")
        {
            rb.velocity = Vector3.zero;
            t = 0;
            start = false;
        }
    }
}
