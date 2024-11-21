using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Rigidbody myRb;
    public float forceRequired = 1f;
    
    private LevelManager levelManager;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        
        levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 collisionForce = other.impulse / Time.fixedDeltaTime;
        
        if (collisionForce.magnitude >= forceRequired)
        {
            levelManager.score += GetComponent<ScoreScript>().scoreAdd;
            levelManager.targetsLeft--;
                
            Destroy(this.gameObject);
        }
    }
}
