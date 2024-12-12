using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float liveTime = 10f;

    public bool startTimer;
    
    public Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        if (startTimer)
            liveTime -= Time.deltaTime;
        
        if (liveTime <= 0)
            Destroy(gameObject);
    }
}
