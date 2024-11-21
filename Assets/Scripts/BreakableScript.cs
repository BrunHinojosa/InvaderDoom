using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    private Rigidbody myRb;
    public float forceRequired = 2f;

    private LevelManager levelManager;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        
        levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 collisionForce = other.impulse / Time.fixedDeltaTime;
        
        forceRequired -= collisionForce.magnitude;
            
        if (forceRequired <= 0)
        {
            levelManager.score += GetComponent<ScoreScript>().scoreAdd;
                
            Destroy(this.gameObject);
        }
    }
}
