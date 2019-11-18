﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawn : MonoBehaviour
{
    public GameObject prefabSound;
    
    public bool destroyWhenDone = true;
    public bool soundPrefabIsChild = true;
    [Range(0.01f, 10f)]
    public float pitchRandomMultiplier = 1f;
    
    // Use this for initialization
    void Awake()
    {
        //Spawn the sound object
        GameObject m_Sound = Instantiate(prefabSound, transform.position, Quaternion.identity);
        AudioSource m_Source = m_Sound.GetComponent<AudioSource>();
    
        //Attach object to parent if true
        if (soundPrefabIsChild)
            m_Sound.transform.SetParent(transform);
    
        //Multiply pitch
        if (pitchRandomMultiplier != 1)
        {
            if (Random.value < .5)
                m_Source.pitch *= Random.Range(1 / pitchRandomMultiplier, 1);
            else
                m_Source.pitch *= Random.Range(1, pitchRandomMultiplier);
        }
    
        //Set lifespan if true
        if (destroyWhenDone)
        {
            float life = m_Source.clip.length / m_Source.pitch;
            Destroy(m_Sound, life);
        }
    }
}