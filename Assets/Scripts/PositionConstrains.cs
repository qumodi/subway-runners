using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PositionConstrains : MonoBehaviour
{
    public float[] allowedX = new float[]{-2,0,2}; 
    public float y = 0;
    private float z = 2.75f;//5.5f

    public static bool redact;
    // Update is called once per frame
    void Update()
    {
        
        if(redact){
            float newX = FindClosestAllowedX(transform.position.x);
            float newZ = Mathf.Round(transform.position.z / z) * z;

            transform.position = new Vector3(newX, y, newZ);
        }
    }

    void OnValidate(){
        
    }

    float FindClosestAllowedX(float currentX)
    {
        float closestX = allowedX[0];
        float minDistance = Mathf.Abs(currentX - closestX);

        foreach (float allowedX in allowedX)
        {
            float distance = Mathf.Abs(currentX - allowedX);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestX = allowedX;
            }
        }
        
        return closestX;
    }
}
