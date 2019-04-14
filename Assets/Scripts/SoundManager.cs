using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType { 主角死亡, 怪物死亡 }

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("主角死亡, 怪物死亡")]
    public List<AudioSource> sounds;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_Sound(SoundType type)
    {
        if((int)type < sounds.Count)
            sounds[(int)type].Play();
    }
}
