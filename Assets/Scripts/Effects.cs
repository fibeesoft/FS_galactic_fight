using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public static Effects instance;
    public AudioClip [] sounds;
    public GameObject [] effects;
    void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    public void CreateExplosionEffect(Vector3 pos){
        GameObject explosionEffect = GameManager.instance.effectPrefabArray[2];
        GameObject a = Instantiate(explosionEffect, pos, Quaternion.identity);
        Destroy(a, 1f);
    }



    /////sounds////

}
