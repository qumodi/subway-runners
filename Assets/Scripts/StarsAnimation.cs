using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsAnimation : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Player player;
    public List<MeshRenderer> stars = new List<MeshRenderer>();
    public bool Active;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Active){
            foreach(MeshRenderer starRenderer in stars){
                starRenderer.enabled = true;
            }
            rb.AddTorque(transform.up * 20,ForceMode.Impulse);

            Invoke(nameof(Stop),5);
            Active = false;
        }
        
    }

    public void Stop(){
        //this.gameObject.SetActive(false);
        foreach(MeshRenderer starRenderer in stars){
            starRenderer.enabled = false;
        }
        //Active = false;
        player.lifes++;
    }
}
