using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsScript : MonoBehaviour
{   
    [SerializeField] private SoundsManager sounds;
    [SerializeField] private TextMeshProUGUI coinsText;
    private int coinsCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // void OnCollisionEnter(Collision other)
    // {
    //     if(other.gameObject.tag == "Coin"){
    //         Debug.Log("Coin");
    //     }
    // }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin"){
            coinsCount++;
            Destroy(other.gameObject);

            coinsText.text = ConvertToSixDigits(coinsCount);

            Debug.Log("Coin");
            sounds.Coin();
        }
    }

    string ConvertToSixDigits(int num){
        //String baseText = "000000";
        String newText = "";
        for(int i = 5; i >= 0 ;i--){
            newText = num % 10 + newText;
            num/=10;
        }
        return newText;
    }
}
