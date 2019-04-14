using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Weapon> all_weapons;

    public Weapon cur_weapon, next_weapon;

    // Start is called before the first frame update
    void Start()
    {
        cur_weapon = all_weapons[Random.Range(0, all_weapons.Count)];
        next_weapon = all_weapons[Random.Range(0, all_weapons.Count)];
        Refresh_UI_Icon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch_To_Next_Weapon()
    {
        cur_weapon = next_weapon;
        next_weapon = all_weapons[Random.Range(0, all_weapons.Count)];

        Refresh_UI_Icon();
    }

    void Refresh_UI_Icon()
    {
        PlayerUIManager.instance.currentPullOutSprite.sprite = cur_weapon.gameObject.GetComponent<SpriteRenderer>().sprite;
        PlayerUIManager.instance.nextPullOutSprite.sprite = next_weapon.gameObject.GetComponent<SpriteRenderer>().sprite;

        PlayerUIManager.instance.Update_Sprite();
    }


}
