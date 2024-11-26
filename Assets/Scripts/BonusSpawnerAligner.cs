using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawnerAligner : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name.Contains("blocker_jump")){
            transform.position = new Vector3(transform.position.x,1.8f,transform.position.z);
        }else if(other.gameObject.name.Contains("blocker_standart")){
            transform.position = new Vector3(transform.position.x,2f,transform.position.z);
        }else if(other.gameObject.name.Contains("train")){
            transform.position = new Vector3(transform.position.x,3.5f,transform.position.z);
        }
    }
}
