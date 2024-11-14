using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    private Rigidbody myRb;
    public float energyRequired = 2f;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GameobjectScript>() != null)
        {
            energyRequired -= other.gameObject.GetComponent<GameobjectScript>().RelativeKineticEnergy(myRb);
            
            if (energyRequired <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
