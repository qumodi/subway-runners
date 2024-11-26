using System;
//using System.Random;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public struct StringBonusPair{
    public string Name;
    public Barrier bonus;
}

[Serializable]
public struct ObstaclesLengthPair{
    public GameObject obstacle;
    public float length;
}

public class LogicManager : MonoBehaviour
{
    //public List<StringBarrierPair> obstacles = new List<StringBarrierPair>();
    public GameObject forrest1;
    public GameObject forrest2;
    public float forrestLength = 108;
    public List<ObstaclesLengthPair> obstacles;
    public List<Barrier> bonuses;
    private List<int> order = new List<int>{0,1,2,3,4,5,6,7,8,9};//не робити Public бо зламається
    
    public GameObject bonus;
    //[SerializeField] public Dictionary<string,Barrier> obstacles = new Dictionary<string, Barrier>();

    void Start()
    {
        Shuffle(ref order);
        generateArea(new Vector3(0,0,20));

       // int n = UnityEngine.Random.Sh;
        //InvokeRepeating();
        // for(int i = 0 ; i < 5; i++){
        //     GenerateObstacle(new Vector3)
        // }
        // InvokeRepeating(GenerateObstacle,2,2);
        //GenerateColumns();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Vector3 allOfset
    public void generateArea(Vector3 Offset){
        //Debug.Log(order);
        //Debug.Log(obstacles);
        Vector3 newPosition = Offset;

        Instantiate(forrest1,newPosition+ new Vector3(0,0,0),Quaternion.identity);
        Instantiate(forrest2,newPosition + new Vector3(0,0,108),Quaternion.identity);
        Vector3 tempOffset = new Vector3(0,0,0);
        Debug.Log($"Obstacles {obstacles.Count}");
        Debug.Log($"Orders {order.Count}");
        for(int i = 0; i < obstacles.Count; i++){
            //Debug.Log(order[i] );
            //tempOffset += new Vector3(0,0,UnityEngine.Random.Range(1,3));
            //tempOffset += new Vector3(0,0,2);
            newPosition += tempOffset;
            if(i == 0){
                Instantiate(obstacles[order[0]].obstacle,newPosition,Quaternion.identity);
            }else{
                newPosition += new Vector3(0,0,obstacles[order[i-1]].length + 5);
                Instantiate(obstacles[order[i]].obstacle,newPosition,Quaternion.identity);
            }
        }

        GenerateBonuses(Offset);

    }
    
    public void Shuffle(ref List<int> list)  
    {  
        System.Random rng = new System.Random();  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            int value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }

    public void GenerateBonuses(Vector3 generationOffset){

        float step = 41.2f; //41.2f
        float length = 216/step; //4
        for(int i = 1; i<= length ; i++){
            float p = step * i;

            float ZOffset = 0;
            //float ZOffset = UnityEngine.Random.Range(-5,5);
            float XOffset = UnityEngine.Random.Range(-1,2) * 2;
            float YOffset = 0.6f;

            for (int x = -2; x <= 2; x+=2){
                XOffset = x;
                Vector3 position = new Vector3(XOffset,YOffset,p + ZOffset) + generationOffset;
                float range = 0.5f;
            string collisionObject = CheckCollisionObject(position,new Vector3(range,range,range));
            if(collisionObject != null){
                // if(collisionObject.Contains("Coin")){
                //     CheckCollisionObject(position,new Vector3(0.5f,0.5f,0.5f),true);
                // }
                if(collisionObject.Contains("blocker_jump")){
                    position = new Vector3(position.x,1.8f,position.z);
                }else if(collisionObject.Contains("blocker_standard")){
                    position = new Vector3(position.x,2f,position.z);
                }else if(collisionObject.Contains("train")){
                    position = new Vector3(position.x,3.5f,position.z);
                }
            }

            int bonusNumber = UnityEngine.Random.Range(0,bonuses.Count);
            GameObject newObject = Instantiate(bonuses[bonusNumber].gameObject,position,Quaternion.identity);
            //Debug.Log("Collision : " + collisionObject);
            //Debug.Log("New Object" + newObject.name);
            }
            

        }

        
    }

    string CheckCollisionObject(Vector3 position,Vector3 halfExtent,bool DestroyCollisionObject = false){
            Collider[] coll = Physics.OverlapBox(position,halfExtent,Quaternion.identity);
            if(coll.Length > 0){
                if(coll[0].gameObject.tag == "Coin"){
                    coll[0].gameObject.SetActive(false);
                }
                return coll[0].gameObject.name;
            }else{
                return null;
            }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GeneratorCheck"){
            Debug.Log("section created");
            Shuffle(ref order);
            generateArea(new Vector3(0,0,108));
        }
    }
}
