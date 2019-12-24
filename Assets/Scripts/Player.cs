using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject[] BulletPrefabs;
    public Transform aimPosition;  
    bool isPowerBulletShot;
    GameObject bullet;
    Rigidbody2D rb;
    Vector3 pos;
    int playerNumber;

    GameObject playerUI, playerUImain;
    float speed;
    int hp;
    int maxhp;
    int attack;
    int secondAttack;
    float scal;
    SpriteRenderer spriteRend;
    public Sprite[] sprites;
    AudioSource audioSource;
    AudioClip audioClip;
    int skinNumber;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isPowerBulletShot = false;
        bullet = null;
        audioSource = GetComponent<AudioSource>();
        gameObject.transform.localScale *= scal;

    }

    void Update(){
        if(Input.GetButtonDown("Fire1" + playerNumber)){
            if(BulletPrefabs[0] != null){
                Shoot();                
            }
        }

        if(Input.GetButtonDown("Fire2" + playerNumber)){
            ShootPowerBullet();     
         
        }
        if(gameObject != null){
            Move();
        }
    }


    void Move(){
        pos = transform.position;
        float moveX = Input.GetAxisRaw("Horizontal" + playerNumber);
        float moveY = Input.GetAxisRaw("Vertical" + playerNumber);
        Vector3 move = new Vector3(moveX, moveY,0f);
        pos += move.normalized*Time.deltaTime*speed;
        pos = new Vector3(Mathf.Clamp(pos.x, -8f, 8f), Mathf.Clamp(pos.y, -4f, 4f), 0f);
        transform.position = pos;

        if ((Vector2)move != Vector2.zero) {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void updateUI(){
        playerUImain.GetComponentInChildren<Slider>().maxValue =maxhp;
        playerUImain.GetComponentInChildren<Slider>().value =hp;
        playerUImain.GetComponentInChildren<Text>().text = GameManager.instance.playerSkins[skinNumber].pname;

    }
    public void Initialize(int p_skinNumber, int p_playerNumber){
        skinNumber = p_skinNumber;
        speed = GameManager.instance.playerSkins[skinNumber].speed;
        maxhp = GameManager.instance.playerSkins[skinNumber].maxhp;
        attack = GameManager.instance.playerSkins[skinNumber].attack;
        scal = GameManager.instance.playerSkins[skinNumber].scale;
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        spriteRend.sprite = GameManager.instance.playerSkins[skinNumber].img;
        hp = maxhp;
        playerNumber = p_playerNumber;
        playerUI = GameObject.FindGameObjectWithTag("puiImage" + p_playerNumber);
        playerUImain = GameObject.FindGameObjectWithTag("pui" + p_playerNumber);
        playerUI.GetComponent<Image>().sprite = GameManager.instance.playerSkins[skinNumber].img;
        updateUI();
        gameObject.layer = LayerMask.NameToLayer("player" + p_playerNumber);        
    }

    void Die(){
        spriteRend.sprite = null;
        audioClip = GameManager.instance.audioClipsArray[1];
        audioSource.clip = audioClip;
        audioSource.Play();
        CreateExplosion1();
        Destroy(gameObject, 0.5f);
    }



    public void TakeDamage(int damage){
        if(hp > damage){
            hp -= damage;
            audioClip = GameManager.instance.audioClipsArray[4];
            audioSource.clip = audioClip;
            audioSource.Play();
            updateUI();
        }else{
            hp -= damage;
            updateUI();
            Die();
            int winPlayer = gameObject.transform.CompareTag("Player1") ? 2 : 1;
            GameManager.instance.OpenPauseMenu(winPlayer);
        }
    }

    public int MakeDamage(){
        return attack;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Bullet")){
            TakeDamage(System.Convert.ToInt32(other.name.Substring(6,1)));
            rb.AddForce(other.gameObject.GetComponent<Rigidbody2D>().velocity * 30f, ForceMode2D.Impulse);
            Destroy(other.gameObject, 0f);
        }
        if(other.gameObject.CompareTag("ExplosionBullet")){
            TakeDamage(System.Convert.ToInt32(other.gameObject.name.Substring(6,1)));
        }
        if(other.gameObject.CompareTag("BulletEnemy")){
            TakeDamage(System.Convert.ToInt32(other.transform.name.Substring(6,1)));
            Destroy(other.gameObject, 0f);  
        }
        if(other.gameObject.CompareTag("EnemyFollower")){
            //TakeDamage(other.transform.GetComponent<Damage>().GetAttack());  
            TakeDamage(2);
            Destroy(other.gameObject, 0f);
        }            
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Moon")){
            TakeDamage(3);
        }
        if(other.gameObject.CompareTag("EnemyFollower")){
            TakeDamage(other.transform.GetComponent<Damage>().GetAttack());  
        }
        if(other.gameObject.CompareTag("Enemy")){
            GameManager.instance.GameOver(); 
        }
        if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")){
            rb.AddForce(other.gameObject.GetComponent<Rigidbody2D>().velocity * 70000f, ForceMode2D.Impulse);
        }
    }

    void Shoot(){
        audioSource.clip = GameManager.instance.audioClipsArray[5];
        audioSource.Play();
        GameObject a = Instantiate(BulletPrefabs[0], aimPosition.transform.position, Quaternion.identity);
        a.name = "Bullet" + attack;
        if(gameObject.transform.CompareTag("Player1")){
            a.layer = LayerMask.NameToLayer("player_bullets1");
        }else{
            a.layer = LayerMask.NameToLayer("player_bullets2");
        }
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
        bulletExplosion.name = "bulexp" + (attack + 2);

        Destroy(bulletExplosion, 1f);
    }

    void CreateExplosion1(){
        GameObject exp1 = Instantiate(GameManager.instance.effectPrefabArray[2], transform.position, Quaternion.identity);
        Destroy(exp1, 1f);
    }

}
