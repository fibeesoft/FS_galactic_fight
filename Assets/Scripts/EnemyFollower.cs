﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    Vector3 playerPos;
    GameObject player;
    Damage damageScript;
    float speed;
    private void Start() {
        damageScript = GetComponent<Damage>();
        player = GameObject.FindGameObjectWithTag("Player1");
        speed = Random.Range(0.5f, 1.8f);
    }
    void Update()
    {
        if(player != null){
            playerPos = player.transform.position;  
        }
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);        
    }


}
