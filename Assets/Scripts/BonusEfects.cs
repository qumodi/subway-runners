using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public enum Bonus{None,Boots,Magnet,Acceleration}

public class BonusEfects : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private DistanceCounter distanceCounter;
    //Objects on player
    [SerializeField] GameObject LeftBoot,RightBoot;
    [SerializeField] GameObject Magnet;
    [SerializeField] GameObject[] Bars;//boots,multiplier,magnet
    [SerializeField] Image[] TimeBars;
    //[SerializeField] Image abilityImage;
    //[SerializeField] Sprite BootsImage,MultiplierImage,MagnetImage;

    private Vector3 bar0Position = new Vector3(-520,-1400,29);
    private Vector3 bar1Position = new Vector3(149,-1400,29);
    private Vector3 bar2Position = new Vector3(-520,-1177,29);
    private Vector3[] positions = new Vector3[]{};
    public RectTransform[] relativePositions;
    //private V
    private Dictionary<int,int> line = new Dictionary<int, int>{};
    bool activeBoots,activeMagnet,activeMultiplier;
    private float doubleJump ,standardJump;
    private float jumpChange = 1.5f;
    private float resetTime = 10f;
    private float bootsTime,multiplierTime,magnetTime;

    [SerializeField] private Collider magnetZone; 

    private void Start(){
        positions = new Vector3[]{bar0Position,bar1Position,bar2Position};

        for(int i = 0; i < relativePositions.Length; i++){
            positions[i] = relativePositions[i].localPosition;
        }

        distanceCounter = GetComponent<DistanceCounter>();

        standardJump = player.JumpForce.y;
        doubleJump = player.JumpForce.y * jumpChange;

        DisableAllBars();
        
        

        // line.Add(0,0);
        // line.Add(1,1);
        // line.Add(2,2);
    }

    // Update is called once per frame
    void Update()
    {

        if(activeBoots || activeMagnet || activeMultiplier){
            //EnableBar();
        }

        if(activeBoots)
        {   
            if(Bars[0].transform.localPosition != positions[0]){
                Bars[0].transform.localPosition = positions[CheckPosition(0)];
            }

            EnableBar(0);
            bootsTime -= Time.deltaTime;
            float percent = bootsTime / resetTime;
            ChangeBar(0,percent);

            if (bootsTime <= 0)
            {
                activeBoots = false;
                TurnOfBoots();
                DisableBar(0);
            }
        }
        if(activeMultiplier){
            if(Bars[1].transform.localPosition != positions[0]){
                Bars[1].transform.localPosition = positions[CheckPosition(1)];
            }
            //Bars[1].transform.localPosition = positions[CheckPosition(1)];
            EnableBar(1);

            multiplierTime -= Time.deltaTime;
            float percent = multiplierTime / resetTime;
            ChangeBar(1,percent);

            //Debug.Log(bootsTime);
            if(multiplierTime <= 0){
                activeMultiplier = false;
                TurnOfMultiplier();
                DisableBar(1);
            }
        }
        if(activeMagnet){    
            if(Bars[2].transform.localPosition != positions[0]){
                Bars[2].transform.localPosition = positions[CheckPosition(2)];
            }
            EnableBar(2);

            magnetTime -= Time.deltaTime;
            float percent = magnetTime / resetTime;
            ChangeBar(2,percent);

            //Debug.Log(magnetTime);
            if(magnetTime <= 0){
                activeMagnet = false;
                TurnOfMagnet();
                DisableBar(2);
            }
        }
    }

    private void ChangeBar(int index,float percent)
    {
        //Bars[0].transform.Find("TimeBar").gameObject.GetComponent<Image>().fillAmount = percent;
        TimeBars[index].fillAmount = percent;
        float red = (1 - percent);
        float green = percent;
        float blue = 0;

        // Нормалізуємо колір для більшої яскравості
        float maxColorValue = Mathf.Max(red, green, blue);
        if (maxColorValue > 0) // Щоб уникнути ділення на нуль
        {
            red /= maxColorValue;
            green /= maxColorValue;
            blue /= maxColorValue;
        }
        TimeBars[index].color = new Color(red, green, blue);
    }

    private int CheckPosition(int index){
        bool isEmpty = true;
        //int EmptyPosition = 0;
        for(int i = 0 ; i < GetIndexByPosition(Bars[index].transform.localPosition) ;i++){

            // if(positions[i] == Bars[index].gameObject.transform.localPosition){
            //     return i;
            // }
            isEmpty = true;
            foreach(GameObject Ob in Bars){
                // if(index == i){
                //     continue;
                // }
                if(positions[i] == Ob.gameObject.transform.localPosition){
                    isEmpty = false;
                }
            }

            if(isEmpty){
                return i;
            }
        }
        return GetIndexByPosition(Bars[index].transform.localPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "BootsBonus"){
            activeBoots = true;
            bootsTime = resetTime;

            player.JumpForce.Set(0,doubleJump,0);
            LeftBoot.GetComponentInChildren<MeshRenderer>().enabled = true;
            RightBoot.GetComponentInChildren<MeshRenderer>().enabled = true;
            //Invoke(nameof(TurnOfBoots),resetTime);

            other.gameObject.GetComponent<MeshRenderer>().enabled = false;

            player.sounds.PowerUp();
        }

        if(other.gameObject.tag == "MultiplierBonus"){
            activeMultiplier = true;
            multiplierTime = resetTime;

            distanceCounter.speedModificator = 2;
            other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            
            player.sounds.PowerUp();
            //Invoke(nameof(TurnOfMultiplier),resetTime);
        }

        if(other.gameObject.tag == "MagnetBonus"){
            activeMagnet = true;
            magnetTime = resetTime;
            
            magnetZone.enabled = true;
            Magnet.GetComponentInChildren<MeshRenderer>().enabled = true;
            other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

            player.sounds.PowerUp();
            //Invoke(nameof(TurnOfMagnet),resetTime);
        }
    }
    private int GetIndexByPosition(Vector3 position){
        for(int i = 0; i < positions.Length; i++){
            if(position == positions[i]){
                return i;
            }
        }
        return 2;
    }
    public void TurnOfBoots(){
        //activeBoots = false;
        player.JumpForce.Set(0,standardJump,0);
        LeftBoot.GetComponentInChildren<MeshRenderer>().enabled = false;
        RightBoot.GetComponentInChildren<MeshRenderer>().enabled = false;

        player.sounds.PowerDown();

    }
    public void TurnOfMultiplier(){
        distanceCounter.speedModificator = 1;

        player.sounds.PowerDown();
    }
    public void TurnOfMagnet(){
        magnetZone.enabled = false;
        Magnet.GetComponentInChildren<MeshRenderer>().enabled = false;

        player.sounds.PowerDown();
    }

    public void DisableBar(int index){
        Image[] images = Bars[index].GetComponentsInChildren<Image>();
        foreach(Image im in images){
            //Debug.Log(im);
            im.enabled = false;
        }
        Bars[index].transform.position = new Vector3(0,0,0);
    }

    public void DisableAllBars(){
        foreach(GameObject Ob in Bars){
            Image[] images = Ob.GetComponentsInChildren<Image>();
            foreach(Image im in images){
                im.enabled = false;
            }
            Ob.transform.position = new Vector3(0,0,0);
        } 
        activeBoots = activeMultiplier = activeMagnet = false;
        bootsTime = multiplierTime = magnetTime = 0;
    }

    public void EnableBar(int index){
        Image[] images = Bars[index].GetComponentsInChildren<Image>();
        foreach(Image im in images){
            im.enabled = true;
        }
        // abilityImage.enabled = true;
        // BackgroundBar.enabled = true;
        // TimeBar.enabled = true;
        // EmptyBar.enabled = true;
    }
}
