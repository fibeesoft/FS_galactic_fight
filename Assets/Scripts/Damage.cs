using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    int hp;
    SpriteRenderer spriteRend;
    AudioSource audioSource;

    void Start()
    {
        hp = AssignMaxHp();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    int AssignMaxHp(){
        int maxhp;
        if(gameObject.CompareTag("Player")){
            maxhp = 10;
        }else if(gameObject.CompareTag("Enemy")){
            maxhp = 10;
        }else if(gameObject.CompareTag("EnemyFollower")){
            maxhp = 2;
        }else{
            maxhp = 1;
        }

        return maxhp;
    }
    void Update()
    {
        
    }

    public void TakeDamage(int damage){
        if(hp - damage > 0){
            hp-=damage;
            print(gameObject.name + " " + hp);
            Hit();
        }else{
            Die();
        }
    }

    void Hit(){
        AudioClip audioclip = GameManager.instance.audioClipsArray[3];
        audioSource.clip = audioclip;
        audioSource.Play();        
    }

    public void Die(){
        spriteRend.sprite = null;
        GetComponent<CircleCollider2D>().enabled = false;
        CreateExplosionEffect();
        AudioClip audioclip = GameManager.instance.audioClipsArray[2];
        audioSource.clip = audioclip;
        audioSource.Play();


        if(gameObject.CompareTag("Player")){
            GameManager.instance.GameOver();
        }
        if(gameObject.CompareTag("Enemy")){
            GameManager.instance.WinGame();
        }

        Destroy(gameObject, 1f);
    }

    void CreateExplosionEffect(){
        GameObject explosionEffect = GameManager.instance.effectPrefabArray[0];
        GameObject a = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(a, 1f);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        print(other.gameObject.name);
        if(other.gameObject.CompareTag("Bullet")){
             TakeDamage(1);   
        }
        if(other.gameObject.CompareTag("BulletEnemy")){
             TakeDamage(1);   
        }
        if(!gameObject.CompareTag("Player") ){          
            if(other.gameObject.CompareTag("ExplosionBullet")){
                TakeDamage(2);   
            }
        }
        if(!gameObject.CompareTag("EnemyFollower") && !gameObject.CompareTag("Enemy")){
            if(other.gameObject.CompareTag("EnemyFollower")){
                Die();
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D other) {
        

        if(other.gameObject.CompareTag("Enemy")){
            Die();
        }
        if(other.gameObject.CompareTag("Player")){
            Die();
        }

    }
}
