using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    private Rigidbody myRb;
    public float forceRequired = 2f;

    private LevelManager levelManager;

    public float scoreAdd = 1f;

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
            levelManager.score += scoreAdd;
                
            Destroy(this.gameObject);
        }
    }
}
