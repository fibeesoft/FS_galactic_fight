using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject[] BulletPrefabs;
    public Transform aimPosition;  
    bool isPowerBulletShot;
    GameObject bullet;
    void Start()
    {
        isPowerBulletShot = false;
        bullet = null;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            if(BulletPrefabs[0] != null){
                Shoot();                
            }
        }

        if(Input.GetButtonDown("Fire2")){
            ShootPowerBullet();     
         
        }
    }

    void Shoot(){
        GameObject a = Instantiate(BulletPrefabs[0], aimPosition.transform.position, Quaternion.identity);
        a.GetComponent<Rigidbody2D>().AddRelativeForce(aimPosition.right * 3000*Time.deltaTime, ForceMode2D.Impulse);
        Destroy(a, 3f);
    }
    void ShootPowerBullet(){

        if(isPowerBulletShot == false){
            bullet = Instantiate(BulletPrefabs[1], aimPosition.transform.position, Quaternion.identity);
            //bullet.GetComponent<Rigidbody2D>().AddRelativeForce(aimPosition.right * 1200 *Time.deltaTime, ForceMode2D.Impulse);
            isPowerBulletShot = true;  
        }else{
            CreateBulletExplosion();
            Destroy(bullet, 0f);
            isPowerBulletShot = false;
        }

    }

    void CreateBulletExplosion(){
        GameObject bulletExplosion = Instantiate(GameManager.instance.effectPrefabArray[1], bullet.transform.position, Quaternion.identity);

    }

}
