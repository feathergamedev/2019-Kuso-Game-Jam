using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerUIManager : MonoBehaviour
{
    public Image nextPullOutSprite;
    public Image currentPullOutSprite;

    Vector3 targetPos;
    private void Start()
    {
        targetPos = nextPullOutSprite.transform.position;
    }
#if UNITY_EDITOR 
    private void Update()
    {
         if (Input.GetMouseButtonDown(0))
         {
           UpdatePullOutThing(nextPullOutSprite.sprite);
         }
    }
#endif
    // Start is called before the first frame update
    public void UpdatePullOutThing(Sprite next)
    {
        currentPullOutSprite.sprite = nextPullOutSprite.sprite;
        nextPullOutSprite.sprite = next;
        //DOTween.Play(nextPullOutSprite.gameObject);
        DOTween.Restart(nextPullOutSprite.gameObject);
    }

    public void ChangeSprite()
    {
        Sprite tmpsp = currentPullOutSprite.sprite;
        currentPullOutSprite.sprite = nextPullOutSprite.sprite;
        nextPullOutSprite.sprite = tmpsp;
    }

    
}
