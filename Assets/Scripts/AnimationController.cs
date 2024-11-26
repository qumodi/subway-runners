using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Player player;
    public bool canAnimateArc = false;

    void Start()
    {
        //player = gameObject.GetComponent<Player>();
        //anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // if(player.state == State.Running){
        //     player.transform.position = new Vector3(player.transform.position.x,0.f,player.transform.position.z);
        // }else{
        //     player.transform.position = new Vector3(player.transform.position.x,0,player.transform.position.z);
        // }
        if(player.state == State.Dying){
            player.coll.height = 1;
            //this.transform.position = this.transform.position + new Vector3(0,-0.5f,0);
            anim.SetBool("Dying",true);
        }
        
        if(!player.Alive){
            player.coll.height = 1;
        }else{

            if(player.state == State.Running){
                if(player.direction == Direction.Left && canAnimateArc){
                    anim.SetBool("RunLeft",true);
                    anim.SetBool("RunRight",false);
                }else if(player.direction == Direction.Right && canAnimateArc){
                    anim.SetBool("RunLeft",false);
                    anim.SetBool("RunRight",true);
                }else{
                    anim.SetBool("RunLeft",false);
                    anim.SetBool("RunRight",false);
                    //anim.SetBool()
                }
                
            }
        

            if(player.state == State.Rolling){
                anim.SetBool("IsRolling",true);
                anim.SetBool("Running",false);
                anim.SetBool("Jumping",false);

                player.coll.height = 1;
                player.coll.center = new Vector3(0,-0.5f,0);
            }else{
                player.coll.height = 2;
                player.coll.center = new Vector3(0,0,0);

            }
            if(player.state == State.Jumping){
                if(player.touchGround){
                RunState();
                }else{
                anim.SetBool("Jumping",true);
                anim.SetBool("Running",false);
                anim.SetBool("IsRolling",false);
                }
            }
        }
    }

    public void RunState(){
        Debug.Log("State Restarted");
        player.state = State.Running;
        anim.SetBool("Running",true);
        anim.SetBool("IsRolling",false);
        anim.SetBool("Jumping",false);
        anim.SetBool("RunLeft",false);
        anim.SetBool("RunRight",false);
    }

    public void StopArcAnimation(){
        canAnimateArc = false;
    }

    public void StartArcAnimation(){
        canAnimateArc = true;
    }

    public void StopDying(){
        RunState();
        anim.SetBool("Dying",false);
    }
}
