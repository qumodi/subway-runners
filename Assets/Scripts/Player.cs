using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;

//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public enum Direction{Tapped, Up, Down, Left, Right}
public enum State{Running,Jumping,Falling,Rolling,Dying};


public class Player : MonoBehaviour
{
    [SerializeField] public AnimationController animCtrl;
    [SerializeField] private CameraController cameraCtrl;
    [SerializeField] private StarsAnimation stars;
    [SerializeField] private BonusEfects effects;
    [SerializeField] public SoundsManager sounds;
//movement
    private Rigidbody rb;
    public CapsuleCollider coll;
    private Touch theTouch;
    public Vector3 touchStartPosition, touchEndPosition;
    public Vector3 PlayerStartPosition,PlayerEndPosition;
    public Direction direction;
    public State state = State.Running;

    public Vector3 JumpForce = new Vector3(0,12,0);
    public float speed = 10f;
    public bool canInput = true;
    public bool touchGround = true;

//borders of movement
    public float leftBorder = -3;
    public float leftLine = -2;
    public float middleLine = 0;
    public float rightLine = 2;
    public float rightBorder = 3;

    //private Transform basicTransform;
    public float colliderRadius = 0.4f;
    public int lifes = 2; 
    public bool Alive = true;

    private bool FirstFrames = true;
    private int FramesCount = 0;
    private void Start()
    {
        //basicTransform = Transform.
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        state = State.Running;
        //touchStartPosition = touchEndPosition = Vector3.zero;
        //effects = GetComponent<BonusEfects>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Alive){
            if(FirstFrames){
                touchStartPosition = touchEndPosition = new Vector3(0,0,0);
                if(FramesCount >= 25){
                    FirstFrames = false;
                }else{
                    FramesCount++;
                }
            }else{
                GetInput();
            }
            Move();
            
        }
        //ChangeState();
        //displayText.text = direction.ToString();
    }

    private void GetInput(){
        if(Input.touchCount > 0){
            theTouch = Input.GetTouch(0);

            if(theTouch.phase == TouchPhase.Began){
                touchStartPosition = theTouch.position;
                PlayerStartPosition = new Vector3(FindNearestLine(transform.position),transform.position.y,0);

                animCtrl.canAnimateArc = true;

            }else if(theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended){//
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if(Vector3.Distance(touchStartPosition,theTouch.position) > 0.1 && canInput){
                    if(x == 0 && y == 0){
                        direction = Direction.Tapped;
                    }else if(MathF.Abs(x) > Mathf.Abs(y)){
                        direction = x > 0 ? Direction.Right : Direction.Left ;
                    }else{
                        direction = y > 0 ? Direction.Up : Direction.Down ;
                    }
                    canInput = false;

                    if(direction != Direction.Tapped){
                        sounds.makeDashSound();
                    }
                }

                
            }
        }
        if(Input.touchCount == 0 ){
            canInput = true;
        }
    }

    private void Move(){
        
        switch(direction){
            case Direction.Up:
                if(touchGround){
                rb.AddForce(JumpForce,ForceMode.Impulse);
                touchGround = false;
                state = State.Jumping;
                } 
                direction = Direction.Tapped;
                break;
            case Direction.Down:
                rb.AddForce(-2 * JumpForce,ForceMode.Impulse);
                state = State.Rolling;                                                                                                                                        
                //ChangeState();
                direction = Direction.Tapped;
                break;

            case Direction.Left:
                if(PlayerStartPosition.x == rightBorder){
                    MoveTo(rightLine);
                }else if(PlayerStartPosition.x == rightLine){
                    MoveTo(middleLine);
                }else if(PlayerStartPosition.x == middleLine ){
                    MoveTo(leftLine);
                }else if(PlayerStartPosition.x == leftLine){
                    MoveTo(leftBorder);
                }
                break;
            case Direction.Right:
                if(PlayerStartPosition.x == rightLine){
                    MoveTo(rightBorder);
                }else if(PlayerStartPosition.x == middleLine){
                    MoveTo(rightLine);
                }else if(PlayerStartPosition.x == leftLine ){
                    MoveTo(middleLine);
                }else if(PlayerStartPosition.x == leftBorder){
                    MoveTo(leftLine);
                }
                break;
        }
        //direction = Direction.Tapped;
    }

    private void MoveTo(float endLine){
        PlayerEndPosition = new Vector3(endLine,transform.position.y,transform.position.z);
        transform.position = Vector3.Lerp(transform.position,PlayerEndPosition,speed* Time.deltaTime);

        if(Mathf.Abs(transform.position.x - PlayerEndPosition.x) < 0.1f){
            transform.position = PlayerEndPosition;
            direction = Direction.Tapped;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground"){
            touchGround = true;
        }
        if(other.gameObject.tag == "Blocker"){
            
            if(Math.Abs(other.collider.transform.position.x - this.transform.position.x) > 0.01){
                lifes -= 1;
                Hurt(FindNearestLine(other.transform.position));
                
                Debug.Log("Blocker position : " + other.collider.transform.position);
                Debug.Log("Player position : " + this.transform.position);
            }else {
                Debug.Log("Collision position : " + other.GetContact(0).point + "\n" + "Player position : " + this.transform.position);
                Debug.Log("Distance" + Vector3.Distance(other.GetContact(0).point,this.transform.position));
                if(Vector3.Distance(other.GetContact(0).point,this.transform.position) > colliderRadius){
                    lifes -= 1;
                    Hurt(FindNearestLine(other.transform.position));
                    
                }else if(other.collider.transform.position.z >= this.transform.position.z){
                    lifes -= 2;
                    
                }
            }

            if(lifes < 1){
                effects.DisableAllBars();
                Die();
            }
            Debug.Log("Touch Blocker");
        }
    }

    private void Hurt(float collisionLine){

        animCtrl.canAnimateArc = true;
        cameraCtrl.isShaking = true;

        //PlayerStartPosition = new Vector3(collisionLine,transform.position.y,0);

        Switch(ref PlayerStartPosition,ref PlayerEndPosition);

        if(direction == Direction.Left || direction == Direction.Right){
            if(PlayerStartPosition.x < transform.position.x){
                direction = Direction.Right;
            }
            if(transform.position.x < PlayerStartPosition.x){
                direction = Direction.Left;
            }
        }
        stars.Active = true;

        sounds.Hit();
    }

    public float dyingPower = 5f;
    private void Die(){
        state = State.Dying;
        Barrier.StopMoving();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.AddForce(new Vector3(0,1,-1) * dyingPower ,ForceMode.Impulse);
        rb.velocity.Set(0,rb.velocity.y,rb.velocity.z);
        Alive = false;

        sounds.PowerDown();
        sounds.MetalHit();
    }

    private void Switch(ref Vector3 vec1,ref Vector3 vec2){
        Vector3 Temp = new Vector3(vec1.x,vec1.y,vec1.z);
        vec1 = vec2;
        vec2 = Temp;
        Debug.Log("Switched");
    }

    private void operateLine(){
        float[] lines = new float[]{leftBorder,leftLine,middleLine,rightLine,rightBorder};

    }

    private void ResetState(){
        transform.Rotate(new Vector3(-90,0,0));
        state = State.Running;
    }

    private float FindNearestLine(Vector3 currentPosition){
        float distanceToLeft = Mathf.Abs(currentPosition.x - leftLine);
        float distanceToRight = MathF.Abs(currentPosition.x - rightLine);
        float distanceToMiddle = MathF.Abs(currentPosition.x - middleLine);

        float distanceToLeftBorder = Mathf.Abs(currentPosition.x - leftBorder);
        float distanceToRightBorder = Mathf.Abs(currentPosition.x - rightBorder);

        if(distanceToLeftBorder < distanceToLeft){
            return leftBorder;
        }else if(distanceToRightBorder < distanceToRight){
            return rightBorder;
        }

        if(distanceToLeft < distanceToMiddle){
            return leftLine;
        }else if(distanceToRight < distanceToMiddle){
            return rightLine;
        }else{
            return middleLine;
        }
    }
}
