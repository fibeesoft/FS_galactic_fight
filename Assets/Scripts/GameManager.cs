using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }
    int sceneNumber;
    AudioSource audioSource;
    public GameObject[] effectPrefabArray;
    public AudioClip[] audioClipsArray;

    public PlayerSkin [] playerSkins;
    public Sprite[] skinsImages;
    public GameObject pauseMenu;
    bool isPauseMenuOn;
    void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        isPauseMenuOn = false;
        if(pauseMenu!=null){
            pauseMenu.SetActive(false);
        }
        audioSource = GetComponent<AudioSource>();
        if(audioSource!= null){
            audioSource.clip = audioClipsArray[0];
            audioSource.Play();
        }
        Time.timeScale = 1;
    }

    private void Update() {

        //MainMenu Scene
        if(sceneNumber == 0){
            if(Input.GetButtonDown("Fire2")){   // Fire2 - B
                LoadTheGame2();
            }

            if(Input.GetButtonDown("X")){
                LoadTheGame();
            }  
            if(Input.GetButtonDown("Back")){
                CloseApp();
            }  


        }
        // Game 1 i 2 Scene
        if(sceneNumber == 1 || sceneNumber == 2){
            if(pauseMenu != null){
                if(Input.GetButtonDown("Back")){
                    if(isPauseMenuOn){
                        ClosePauseMenu();
                    }else{
                        OpenPauseMenu();
                    }
                }
                if(isPauseMenuOn){
                    if(Input.GetButtonDown("Fire2")){
                        LaodMainMenu();
                    }
                    if(Input.GetButtonDown("Submit")){
                        ClosePauseMenu();
                    }
                }
            }
        }
        if(isPauseMenuOn){
            if(sceneNumber == 1){
                if(Input.GetButtonDown("X")){
                        LoadTheGame();
                } 
            }
            if(sceneNumber == 2){
                if(Input.GetButtonDown("X")){
                        LoadTheGame2();
                } 
            }
        }

        if(sceneNumber == 3){
            if(Input.GetButtonDown("Fire1")){
                LoadTheGame();
            }
            if(Input.GetButtonDown("Fire2")){
                LaodMainMenu();
            }
        }
        if(sceneNumber == 4){
            if(Input.GetButtonDown("Fire1")){
                LaodMainMenu();
            }
        }

    }

    public void OpenPauseMenu(){
        pauseMenu.SetActive(true);
        isPauseMenuOn = true;
        Time.timeScale = 0;
    }

    public void ClosePauseMenu(){
        pauseMenu.SetActive(false);
        isPauseMenuOn = false;
        Time.timeScale = 1;
    }

    public void LaodMainMenu(){
        SceneManager.LoadScene(0);
    }
    public void LoadTheGame(){
        SceneManager.LoadScene(1);
    }
    public void LoadTheGame2(){
        SceneManager.LoadScene(2);

    }
    public void GameOver(){
        SceneManager.LoadScene(3);
 
    }
    public void WinGame(){
        SceneManager.LoadScene(4);
    }

    public void CloseApp(){
        Application.Quit();
    }




}
