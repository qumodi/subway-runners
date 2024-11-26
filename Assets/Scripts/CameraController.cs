using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Player player;
    public Vector3 startPosition;
    
    public float shakePower = 1;
    public float shakeTime;

    public bool isShaking = true;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        ///transform.localPosition = player.transform.position + startPosition;

        if(isShaking){
            StartCoroutine(ShakeCamera(shakeTime));
        }
    }

    private IEnumerator ShakeCamera(float time){
        isShaking = false;
        float timePassed = 0f;
        while(timePassed < time){
            float x = Random.Range(-1f,1f) * shakePower;
            float y = Random.Range(-1f,1f) * shakePower;

            transform.localPosition = startPosition + new Vector3(x,y,0);

            timePassed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = startPosition;

    }
}
