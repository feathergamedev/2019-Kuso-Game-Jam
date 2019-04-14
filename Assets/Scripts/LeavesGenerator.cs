using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesGenerator : MonoBehaviour
{
    public GameObject leaf;
    public float coolDown;

    public float upperBound;
    public float lowerBound;
    public float leftBound;
    public float rightBound;

    private Color[] colors = { Color.red, Color.green, Color.blue };

    void Start()
    {
        StartCoroutine(generate());
    }

    private IEnumerator generate()
    {
        while(true)
        {
            GameObject go = Instantiate(leaf, new Vector3(Random.Range(leftBound, rightBound), Random.Range(lowerBound, upperBound), 0), Quaternion.Euler(0, 0, Random.Range(-30f, 30f)));
            go.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 2)];
            Destroy(go, 1);
            yield return new WaitForSeconds(coolDown * Random.Range(0.6f, 1f));
        }
    }
}
