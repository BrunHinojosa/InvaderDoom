using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectScript : MonoBehaviour
{
    private Rigidbody myRb;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }
    
    public float RelativeKineticEnergy(Rigidbody rb)
    {
        //mass in kg, velocity in meters per second, result is joules
        return 0.5f * myRb.mass * Mathf.Sqrt(Mathf.Pow(myRb.velocity.x - rb.velocity.x, 2) + Mathf.Pow(myRb.velocity.y - rb.velocity.y, 2));
    }
}
