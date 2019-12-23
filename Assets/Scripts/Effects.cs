using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public static Effects instance;
    AudioSource audioSource;
    void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CreateExplosionEffect(Vector3 pos){
        GameObject explosionEffect = GameManager.instance.effectPrefabArray[2];
        GameObject a = Instantiate(explosionEffect, pos, Quaternion.identity);
        Destroy(a, 1f);
    }



    /////sounds////

    public void DieSoundEffect(){
        AudioClip audioclip = GameManager.instance.audioClipsArray[2];
        audioSource.clip = audioclip;
        audioSource.Play();  
    }

    public void HitSoundEffect(){
        AudioClip audioclip = GameManager.instance.audioClipsArray[3];
        audioSource.clip = audioclip;
        audioSource.Play();        
    }
}
