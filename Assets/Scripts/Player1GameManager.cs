using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1GameManager : MonoBehaviour
{
    public static Player1GameManager instance;
    int p1skin = 0;
    bool isGameOn = false;
    public GameObject basicPlayerGameObject;
    public GameObject playUI;
    public GameObject playerSelectUI;

    public Text txt_name1;
    public Text txt_speed1;
    public Text txt_hp1;
    public Text txt_attack1;
    public Image img1;
    void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Time.timeScale = 0;
        playerSelectUI.SetActive(true);
        ChangeSkin();
    }
    
    private void Update() {
        if(!isGameOn){
            if(Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire21") ){
                if(p1skin < GameManager.instance.playerSkins.Length - 1){
                    p1skin++;

                }else{
                    p1skin = 0;
                }
                ChangeSkin();
            }
        }
        if(Input.GetButtonDown("Submit")){
            if(!isGameOn){
                StartTheGame();
            }
        }
    }

    void CreatePlayer(int playerSkinNumber, int playerNumber){
        Vector3 pos = new Vector3(-8f,0f,0f);
        GameObject p = Instantiate(basicPlayerGameObject, pos, Quaternion.identity );
        p.transform.tag = "Player1";
        p.GetComponent<Player>().Initialize(playerSkinNumber, playerNumber);


    }

    public void StartTheGame(){
              CreatePlayer(p1skin, 1);
              playerSelectUI.SetActive(false);
              isGameOn = true;
              Time.timeScale = 1;
    }

    void ChangeSkin(){
        txt_name1.text = GameManager.instance.playerSkins[p1skin].pname;
        txt_speed1.text = "SPEED       " + GameManager.instance.playerSkins[p1skin].speed.ToString();
        txt_hp1.text = "MAXHP      " + GameManager.instance.playerSkins[p1skin].maxhp.ToString();
        txt_attack1.text = "ATTACK      " + GameManager.instance.playerSkins[p1skin].attack.ToString();
        img1.sprite = GameManager.instance.playerSkins[p1skin].img;
    }
}
