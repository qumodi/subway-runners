using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{

    [SerializeField] private AudioSource tempSounds;
    public AudioClip coin;
    public AudioClip dashSound;
    public AudioClip powerUp;
    public AudioClip powerDown;
    public AudioClip hit;
    public AudioClip metalHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangePitch(float minPitch = 0.9f,float maxPitch= 1.1f){
        tempSounds.pitch = Random.Range(minPitch,maxPitch);
    }

    public void makeDashSound(){
        float minPitch = 0.9f;
        float maxPitch = 1.1f;

        ChangePitch(minPitch,maxPitch);
        tempSounds.Play();
    }

    public void Coin(){
        ChangePitch(1.1f,1.1f);
        tempSounds.PlayOneShot(coin);
    }

    public void PowerUp(){
        ChangePitch();
        tempSounds.PlayOneShot(powerUp);
    }

    public void PowerDown(){
        ChangePitch();
        tempSounds.PlayOneShot(powerDown);
    }

    public void Hit(){
        ChangePitch();
        tempSounds.PlayOneShot(hit);
    }

    public void MetalHit(){
        ChangePitch();
        tempSounds.PlayOneShot(metalHit);
    }
}
