using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private Player player;
    public Camera mainCamera;
    public Button quit,resume,pause;
    public Image background;
    public TextMeshProUGUI countdown;
    bool startCountDown = false;
    float maxTime = 5f;
    public float currentTime;
    bool FirstFrame = true;
    // Start is called before the first frame update

    //  void Awake()
    //  {
    //     startCountDown = false; 
    //  }
    void Start()
    {
        Pause();
        HideMenuButtons();
        currentTime = maxTime;
        Resume();
    }

    void Update()
    {
        if(FirstFrame){
            Resume();
            FirstFrame = false;
        }
        if(player.Alive){
            if(startCountDown){
                currentTime -= Time.unscaledDeltaTime;
                HideMenuButtons();
                countdown.enabled = true;
            }
            
            if(currentTime <= 0.5 ){
                countdown.enabled = false;
                UnPause();
                startCountDown = false;
            }
            countdown.text = Mathf.Round(currentTime).ToString();
        }else{
            //HideMenuButtons();
        }
    }

    public void Pause(){
        ShowMenuButtons();
        Debug.Log("time Stopped");
        currentTime = maxTime;
        Time.timeScale = 0;
    }

    public void UnPause(){
        Time.timeScale = 1;
    }

    public void ShowMenuButtons(){
        background.enabled = true;
        quit.image.enabled = true;
        resume.image.enabled = true;
        pause.image.enabled = false;
    }
    public void HideMenuButtons(){
        background.enabled = false;
        quit.image.enabled = false;
        resume.image.enabled = false;
        pause.image.enabled = true;
    }

    public void Quit(){
        Application.Quit();
    }

    public void Resume(){
        startCountDown = true;
        currentTime = maxTime;
    }
}
