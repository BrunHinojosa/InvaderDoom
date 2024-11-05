using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    private float clickRadius = 3f;
    private Camera camera;
    public GameObject dragCircle;

    private bool dragging = false;

    public GameObject[] projectiles;

    private Queue<GameObject> projectileQueue;

    private float forceMultiplier = 250f;
    void Start()
    {
        camera = Camera.main;
        projectileQueue = new Queue<GameObject>(projectiles);
    }
    
    void Update()
    {
        Controls();
    }

    private void Controls()
    {
        Vector3 camWrldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        
        float mouseDistance = Mathf.Sqrt(Mathf.Pow(camWrldPos.x - transform.position.x, 2) + 
                                         Mathf.Pow(camWrldPos.y - transform.position.y, 2));
        
        if (!dragging && projectileQueue.Count != 0 && mouseDistance <= clickRadius && Input.GetMouseButtonDown(0))
        {
            dragging = true;
            
            projectileQueue.Peek().gameObject.transform.SetParent(dragCircle.transform);
            projectileQueue.Peek().gameObject.transform.localPosition = Vector3.zero;
        }
        
        float adjacent = camWrldPos.x - transform.position.x;
        float opposite = camWrldPos.y - transform.position.y;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(adjacent, 2) + Mathf.Pow(opposite, 2));
            
        Vector3 direction = new Vector3(adjacent / hypotenuse, opposite / hypotenuse, 0f);
        
        if (mouseDistance > clickRadius)
            mouseDistance = clickRadius;
        
        if (dragging && Input.GetMouseButton(0))
        {
            dragCircle.SetActive(true);
            
            dragCircle.transform.position = new Vector3(direction.x * mouseDistance, direction.y * mouseDistance, 0f);
        }

        if (dragging && Input.GetMouseButtonUp(0))
        {
            dragCircle.SetActive(false);
            dragging = false;
            
            projectileQueue.Peek().gameObject.transform.SetParent(null);
            projectileQueue.Peek().gameObject.GetComponent<Rigidbody>().useGravity = true;
            projectileQueue.Peek().gameObject.GetComponent<Rigidbody>().AddForce(-direction * (mouseDistance * forceMultiplier));
            
            projectileQueue.Dequeue();
        }
    }
}
