﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    int hp;
    int maxhp;
    int attack;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AssignMaxHpAndAttack(){
        if(gameObject.CompareTag("Enemy")){
            maxhp = 36;
            attack = 4;
        }else if(gameObject.CompareTag("EnemyFollower")){
            maxhp = 2;
            attack = 3;
        }else{
            maxhp = 1;
            attack = 1;
        }
        hp = maxhp;
    }
    public int GetMaxHp(){
        return maxhp;
    }
    public int GetHp(){
        return hp;
    }
    public int GetAttack(){
        return attack;
    }
    public void TakeDamage(int damage){
        if(hp - damage > 0){
            hp-=damage;
            int randomNumber = Random.Range(3,5);
            audioSource.clip = Effects.instance.sounds[randomNumber];
            audioSource.Play();
            if(gameObject.transform.CompareTag("Enemy")){
                GetComponent<Enemy>().updateUI();
            }
        }
        else{
            Die();
        }
    }

    public int MakeDamage(){
        return attack;
    }

    public void Die(){
        if(gameObject.transform.CompareTag("Enemy")){
            GetComponent<Enemy>().Die();
        }else{
            Effects.instance.CreateExplosionEffect(gameObject.transform.position);
            audioSource.clip = Effects.instance.sounds[1];
            audioSource.Play();
            DestroyColliderAndImage();
            Destroy(gameObject, 0.5f);
        }
    }

    void DestroyColliderAndImage(){
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if(other.transform.GetComponent<Damage>() != null){
        //     TakeDamage(other.transform.GetComponent<Damage>().MakeDamage());
        // }

        if(other.transform.CompareTag("Bullet")){
            TakeDamage(System.Convert.ToInt32(other.transform.name.Substring(6,1)));   
        }
        if(other.transform.CompareTag("ExplosionBullet")){
            TakeDamage(System.Convert.ToInt32(other.transform.name.Substring(6,1)));   
        }

        if(other.transform.CompareTag("Player1")){
            TakeDamage(other.GetComponent<Player>().MakeDamage());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.GetComponent<Damage>() != null){
            TakeDamage(other.transform.GetComponent<Damage>().MakeDamage());
        }

        if(other.transform.CompareTag("Bullet")){
            TakeDamage(System.Convert.ToInt32(other.transform.name.Substring(6,1)));   
        }

        if(other.transform.CompareTag("Player1")){
            TakeDamage(other.transform.GetComponent<Player>().MakeDamage());
        }
    }
}
