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
    public List<AudioSource> weapon_sounds;

    public Weapon cur_weapon, next_weapon;

    public AudioSource cur_sound, next_sound;

    // Start is called before the first frame update
    void Start()
    {
        int cur_idx = Random.Range(0, all_weapons.Count);
        int next_idx = Random.Range(0, all_weapons.Count);

        cur_weapon = all_weapons[cur_idx];
        next_weapon = all_weapons[next_idx];

        Refresh_UI_Icon();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch_To_Next_Weapon()
    {
        int cur_idx = Random.Range(0, all_weapons.Count);

        cur_weapon = next_weapon;
        cur_sound = next_sound;

        int next_idx = Random.Range(0, all_weapons.Count);
        next_weapon = all_weapons[next_idx];
        next_sound = weapon_sounds[next_idx];

        Refresh_UI_Icon();
    }

    void Refresh_UI_Icon()
    {
        PlayerUIManager.instance.currentPullOutSprite.sprite = cur_weapon.gameObject.GetComponent<SpriteRenderer>().sprite;
        PlayerUIManager.instance.nextPullOutSprite.sprite = next_weapon.gameObject.GetComponent<SpriteRenderer>().sprite;

        PlayerUIManager.instance.Update_Sprite();
    }

    public void Play_Sound()
    {
        if(cur_sound != null)
            cur_sound.Play();
    }

}
