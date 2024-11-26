using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image restartBackground;
    [SerializeField] private TextMeshProUGUI restartGuide;


    public float lastTouchTime;
    public float doubleTapTime = 0.2f;
    // Update is called once per frame
    void Update()
    {
        if(!player.Alive){
            restartBackground.enabled = true;
            restartGuide.enabled = true;

            lastTouchTime += Time.deltaTime;

            if(Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began){
                    if(lastTouchTime < doubleTapTime){
                        Barrier.StartMoving();
                        player.animCtrl.RunState();
                        player.state = State.Running;

                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }else{
                        lastTouchTime = 0;
                    }
                }
                
            }
        }
        
    }
}
