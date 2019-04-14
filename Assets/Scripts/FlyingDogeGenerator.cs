using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDogeGenerator : MonoBehaviour
{
    public GameObject doge;
    public int dogeCountLimit;
    public float coolDown;
    public float upperBound;
    public float lowerBound;
    public float leftBound;
    public float RightBound;
    public float prob;

    private Queue<GameObject> dogePool = new Queue<GameObject>();
    private int dogeCount;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 initPos = new Vector3(RightBound, upperBound, 0);

        for(int i=0 ; i<dogeCountLimit ; i++)
        {
            GameObject newBornDogeObject = Instantiate(doge, initPos, Quaternion.identity);
            FlyingDoge theDoge = newBornDogeObject.GetComponent<FlyingDoge>();
            theDoge.generator = this;
            dogePool.Enqueue(newBornDogeObject);
        }

        StartCoroutine(generateDoge());
    }

    public void arrive(FlyingDoge theDoge)
    {
        theDoge.canMove = false;
        theDoge.reset();

        dogePool.Enqueue(theDoge.gameObject);
        dogeCount--;
    }

    private IEnumerator generateDoge()
    {
        while(true)
        {
            // gen
            if(dogeCount < dogeCountLimit && Random.Range(0f, 1f) < prob)
            {
                FlyingDoge nextDoge = dogePool.Dequeue().GetComponent<FlyingDoge>();
                nextDoge.canMove = true;
                nextDoge.reset();
                nextDoge.startFly();

                dogeCount++;
            }

            yield return new WaitForSeconds(coolDown);
        }
    }
}
