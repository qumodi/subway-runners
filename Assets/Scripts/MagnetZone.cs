using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetZone : MonoBehaviour
{

    [SerializeField]private Transform parent;
    public List<Transform> coins;
    public float speed = 11f;
    // Start is called before the first frame update
    void Start()
    {
        //parent = GetComponentInParent<Transform>();
        coins = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!parent.GetComponent<Player>().Alive){
            coins.Clear();
        }
        DeleteNullValues();

        foreach(Transform tr in coins){
            Vector3 move = parent.position - tr.position;
            
            tr.position += move.normalized * Time.deltaTime * speed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin"){
            coins.Add(other.gameObject.transform);
        }
    }

    void DeleteNullValues(){
        // foreach(Transform tr in coins){
        //     if(tr == null){
        //         coins.Remove(tr);
        //     }
        // }

        for(int i = 0 ; i < coins.Count ; i++){
            if(coins[i] == null){
                coins.RemoveAt(i);
                i--;
            }
        }
    }
}
