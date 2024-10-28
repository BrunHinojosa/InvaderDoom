using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveScript : MonoBehaviour
{
    public float moveAcc = 1f;
    public float jumpAcc = 1f;
    public float maxSpeed = 1f;
    
    private Rigidbody rb;

    public bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        RaycastHit hit;
        
        float diameter = transform.TransformPoint(Vector3.right).x - transform.TransformPoint(Vector3.left).x;
        
        if (Physics.Raycast(transform.TransformPoint(Vector3.down / 2), Vector3.down, out hit, transform.TransformPoint(Vector3.left / 2).y - transform.TransformPoint(Vector3.down / 2).y))
        {
            Debug.DrawLine(transform.TransformPoint(Vector3.down / 2), hit.point, Color.red);
        }
        
        if (rb.velocity.y <= 0 && Physics.Raycast(transform.position, Vector3.down, out hit, transform.TransformPoint(Vector3.right / 2).y - transform.TransformPoint(Vector3.down / 2).y))
        {
            grounded = true;
            
            if (!grounded)
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);//This fixes the height of the object when falling onto the ground
        }
        else
        {
            grounded = false;
        }
    }
}
