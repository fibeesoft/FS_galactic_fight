using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject enemyBulletPrefab;
    public Transform aimPosition;
    float shootingRate;
    float timer;
    float bulletSpeed;
    float speed = 1.5f;
    float maxMove = 4f;
    Damage damageScript;

    public bool isMoving, isShooting;
    void Start()
    {
        damageScript = GetComponent<Damage>();
        isShooting = true;
        isMoving = true;
        timer = 0;
        bulletSpeed = 3000f;
        shootingRate = 1.5f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > shootingRate){
            if(enemyBulletPrefab != null){
                if(isShooting){
                    Shoot();
                }

            }
            timer = 0;
        }
        if(isMoving){
            transform.position = new Vector3(transform.position.x,  maxMove * Mathf.Sin(Time.time * speed), 0f);
        }
    }

    void Shoot(){
        GameObject a = Instantiate(enemyBulletPrefab, aimPosition.transform.position, Quaternion.identity);
        a.GetComponent<Rigidbody2D>().AddRelativeForce(-aimPosition.right * bulletSpeed*Time.deltaTime, ForceMode2D.Impulse);
        Destroy(a, 3f);
    }

}
