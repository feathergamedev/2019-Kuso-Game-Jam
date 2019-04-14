using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    public Image nextPullOutSprite;
    public Image currentPullOutSprite;

    Vector3 targetPos;

    private void Awake()
    {
        instance = this;


    }

    private void Start()
    {
//        targetPos = next_weapon.transform.position;
    }
#if UNITY_EDITOR 
    private void Update()
    {
         if (Input.GetMouseButtonDown(0))
         {
//           UpdatePullOutThing(nextPullOutSprite);
         }

    }
#endif
    /*
        // Start is called before the first frame update
    public void UpdatePullOutThing(Sprite next)
    {
        currentPullOutSprite = nextPullOutSprite;
        nextPullOutSprite = next;
        //DOTween.Play(nextPullOutSprite.gameObject);
        DOTween.Restart(next_weapon);
    }

    public void ChangeSprite()
    {
        Sprite tmpsp = currentPullOutSprite;
        currentPullOutSprite = nextPullOutSprite;
        nextPullOutSprite = tmpsp;
    }
    */

    public void Update_Sprite()
    {
        DOTween.Restart(nextPullOutSprite.gameObject);
    }

}
