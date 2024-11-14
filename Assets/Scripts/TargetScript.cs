using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Rigidbody myRb;
    public float energyRequired = 0.1f;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GameobjectScript>() != null)
        {
            if (other.gameObject.GetComponent<GameobjectScript>().RelativeKineticEnergy(myRb) >= energyRequired)
                Destroy(this.gameObject);
        }
    }
}
