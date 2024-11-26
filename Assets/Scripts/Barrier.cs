using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Rigidbody rb;

    public static float StartSpeed = 8;
    public static float speed = StartSpeed;
    public float thisSpeed;
    public bool fast;
    public static bool Active = true;
    public float destroyDistance = -20f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
    }

    void FixedUpdate()
    {

        if(fast){
            thisSpeed = speed * 1.5f;
        }else{
            thisSpeed = speed;
        }

        if(Active){
        //Vector3 newPosition = Vector3.MoveTowards(transform.position, new Vector3(0,0,-11),speed * Time.fixedDeltaTime );
            Vector3 newPosition = new Vector3(transform.position.x,transform.position.y, transform.position.z - thisSpeed * Time.fixedDeltaTime);
            transform.position = newPosition;
        
            if(transform.position.z < destroyDistance){
                Destroy(this.gameObject);
            }
        }
    }

    public static void StopMoving(){
        Active = false;
    }

    public static void StartMoving(){
        Active = true;
    }
}
