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
    public List<Sprite> weapon_icons;

    public Weapon cur_weapon;

    public AudioSource cur_sound;

    public int cur_idx, next_idx;


    // Start is called before the first frame update
    void Start()
    {
        cur_idx = Random.Range(0, all_weapons.Count);
        next_idx = Random.Range(0, all_weapons.Count);

        PlayerUIManager.instance.currentPullOutSprite.sprite = weapon_icons[cur_idx];
        PlayerUIManager.instance.nextPullOutSprite.sprite = weapon_icons[next_idx];
        PlayerUIManager.instance.Update_Sprite();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch_To_Next_Weapon()
    {

        cur_idx = next_idx;

        next_idx = Random.Range(0, all_weapons.Count);

        Icon_Update();


    }

    void Icon_Update()
    {
        PlayerUIManager.instance.currentPullOutSprite.sprite = weapon_icons[cur_idx];
        PlayerUIManager.instance.nextPullOutSprite.sprite = weapon_icons[next_idx];
        PlayerUIManager.instance.Update_Sprite();
    }

    public void Play_Sound()
    {
        if(cur_sound != null)
            cur_sound.Play();
    }

}
