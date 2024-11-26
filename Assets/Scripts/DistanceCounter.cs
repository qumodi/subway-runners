using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private TextMeshProUGUI distanceText;
    public int speedModificator = 1;
    public int basicModifier = 10;
    public int distance;


    void FixedUpdate()
    {
        if(player.Alive){
            distance += 1 * speedModificator;
            distanceText.text ="x" + speedModificator + "  " + ConvertToSixDigits(distance);
        }

        float speedAcceleration = (int)(distance / 100) * 0.1f;
        Barrier.speed = Barrier.StartSpeed + speedAcceleration;
    }

    string ConvertToSixDigits(int num){
        String newText = "";
        for(int i = 5; i >= 0 ;i--){
            newText = num % 10 + newText;
            num/=10;
        }
        return newText;
    }
}
