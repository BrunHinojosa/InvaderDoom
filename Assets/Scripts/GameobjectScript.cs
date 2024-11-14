using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectScript : MonoBehaviour
{
    private Rigidbody myRb;

    void Start()
    {
        if (GetComponent<Rigidbody>() != null)
            myRb = GetComponent<Rigidbody>();
    }
    
    //mass in kg, velocity in meters per second, result is joules
    public float RelativeKineticEnergy(Rigidbody rb)
    {
        if (rb != null && myRb != null)
            return 0.5f * myRb.mass * Mathf.Sqrt(Mathf.Pow(myRb.velocity.x - rb.velocity.x, 2) + Mathf.Pow(myRb.velocity.y - rb.velocity.y, 2));
        if (rb == null && myRb != null)
            return 0.5f * myRb.mass * Mathf.Sqrt(Mathf.Pow(myRb.velocity.x, 2) + Mathf.Pow(myRb.velocity.y, 2));
        if (rb != null && myRb == null)
            return 0.5f * myRb.mass * Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2));
        return 0f;
    }
}
