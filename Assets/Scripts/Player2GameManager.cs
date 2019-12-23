﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2GameManager : MonoBehaviour
{
    public static Player2GameManager instance;
    int p1skin = 0, p2skin = 0;
    bool isGameOn = false;
    public GameObject basicPlayerGameObject;
    public GameObject playUI;
    public GameObject playerSelectUI;

    public Text txt_name1, txt_name2;
    public Text txt_speed1, txt_speed2;
    public Text txt_hp1, txt_hp2;
    public Text txt_attack1, txt_attack2;
    public Image img1,img2;
    void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
 
        playerSelectUI.SetActive(true);
        ChangeSkin();
    }
    
    private void Update() {
        if(!isGameOn){
            if(Input.GetButtonDown("Fire21")){
                if(p1skin < GameManager.instance.playerSkins.Length - 1){
                    p1skin++;

                }else{
                    p1skin = 0;
                }
                ChangeSkin();
            }
            if(Input.GetButtonDown("Fire22")){
                if(p2skin < GameManager.instance.playerSkins.Length - 1){
                    p2skin++;
                }else{
                    p2skin = 0;
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
        Vector3 pos;
        if(playerNumber == 1){
            pos = new Vector3(-8f,0f,0f);
        }else{
            pos = new Vector3(8f,0f,0f);
        }
        GameObject p = Instantiate(basicPlayerGameObject, pos, Quaternion.identity );
        p.transform.tag = "Player" + playerNumber;
        p.GetComponent<Player>().Initialize(playerSkinNumber, playerNumber);


    }

    public void StartTheGame(){
              CreatePlayer(p1skin, 1);
              CreatePlayer(p2skin, 2);
              playerSelectUI.SetActive(false);
              isGameOn = true;
    }

    void ChangeSkin(){
        txt_name1.text = GameManager.instance.playerSkins[p1skin].pname;
        txt_speed1.text = "SPEED       " + GameManager.instance.playerSkins[p1skin].speed.ToString();
        txt_hp1.text = "MAXHP      " + GameManager.instance.playerSkins[p1skin].maxhp.ToString();
        txt_attack1.text = "ATTACK      " + GameManager.instance.playerSkins[p1skin].attack.ToString();
        img1.sprite = GameManager.instance.playerSkins[p1skin].img;

        txt_name2.text = GameManager.instance.playerSkins[p2skin].pname;
        txt_speed2.text = "SPEED       " + GameManager.instance.playerSkins[p2skin].speed.ToString();
        txt_hp2.text = "MAXHP      " + GameManager.instance.playerSkins[p2skin].maxhp.ToString();
        txt_attack2.text = "ATTACK      " + GameManager.instance.playerSkins[p2skin].attack.ToString();
        img2.sprite = GameManager.instance.playerSkins[p2skin].img;
    }
}
