using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    GameObject playerUIimage, playerUImain;
    public Transform aimPosition;
    float shootingRate;
    float timer;
    float bulletSpeed;
    float speed = 1.5f;
    float maxMove = 4f;
    int attack;
    Damage damage;
    SpriteRenderer spriteRend;
    void Start()
    {
        damage = GetComponent<Damage>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        timer = 0;
        bulletSpeed = 15f;
        shootingRate = 1.5f;
        damage.AssignMaxHpAndAttack();
        playerUIimage = GameObject.FindGameObjectWithTag("puiImage2");
        playerUImain = GameObject.FindGameObjectWithTag("pui2");
        playerUImain.GetComponentInChildren<Slider>().maxValue = damage.GetMaxHp();
        updateUI();
        playerUImain.GetComponentInChildren<Text>().text = "Enemy";
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > shootingRate){
            if(enemyBulletPrefab != null){
                Shoot();
            }
            timer = 0;
        }
        transform.position = new Vector3(transform.position.x,  maxMove * Mathf.Sin(Time.time * speed), 0f);
    }

    public void updateUI(){
        playerUImain.GetComponentInChildren<Slider>().value = damage.GetHp();
    }

    void Shoot(){
        attack = damage.GetAttack();
        GameObject a = Instantiate(enemyBulletPrefab, aimPosition.transform.position, Quaternion.identity);
        a.name = "bulene" + attack;
        //a.GetComponent<Rigidbody2D>().AddRelativeForce(-aimPosition.right * bulletSpeed*Time.deltaTime, ForceMode2D.Impulse);
        a.GetComponent<Rigidbody2D>().AddRelativeForce(-aimPosition.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(a, 3f);
    }

    public void Die(){
        GameManager.instance.WinGame();
    }

}
