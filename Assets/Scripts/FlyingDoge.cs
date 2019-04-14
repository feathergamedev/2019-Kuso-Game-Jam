using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDoge : MonoBehaviour
{
    public FlyingDogeGenerator generator;
    public List<Sprite> memes = new List<Sprite>();
    public float upperBound;
    public float lowerBound;
    public float rightBound;
    public float leftBound;
    public float speed;
    public bool canMove;

    private Transform self;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        self = transform;
        canMove = false;
        reset();
    }

    private void Update()
    {
        if(canMove) Move();
    }

    public void startFly()
    {
        // pre-process
    }

    public void Move()
    {
        self.position += Vector3.down * (speed * Time.deltaTime);

        if(self.position.y < lowerBound)
        {
            generator.arrive(this);
        }
    }

    public void reset()
    {
        sr.sprite = memes[Random.Range(0, memes.Count)];
        self.position = new Vector3(Random.Range(leftBound, rightBound), upperBound, 0);
    }
}
