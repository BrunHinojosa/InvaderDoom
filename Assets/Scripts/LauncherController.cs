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

    public Queue<GameObject> projectileQueue;

    private float forceMultiplier = 6f;

    public GameObject testObject;

    private float dotAmount = 50f;
    private List<GameObject> dots;
    List<Vector3> nodes = new List<Vector3>();
    
    void Start()
    {
        camera = Camera.main;
        projectileQueue = new Queue<GameObject>(projectiles);
        
        dots = new List<GameObject>();
        for (int i = 0; i < dotAmount; i++)
        {
            dots.Add(Instantiate(testObject, new Vector3(0f, 0f, 0f), Quaternion.identity));
            dots[i].SetActive(false);
        }

    }
    
    void Update()
    {
        Controls();
    }

    // ReSharper disable Unity.PerformanceAnalysis
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
            
            dragCircle.transform.position = transform.position + new Vector3(direction.x * mouseDistance, direction.y * mouseDistance, 0f);
            
            GenerateSamples(dragCircle.transform.position, -direction, mouseDistance * forceMultiplier, dotAmount, Time.fixedDeltaTime);

            for (int i = 0; i < dots.Count; i += 2)
            {
                dots[i].SetActive(true);
                dots[i].transform.position = nodes[i];
            }
        }

        if (dragging && Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < dots.Count; i += 2)
                dots[i].SetActive(false);
            
            dragCircle.SetActive(false);
            dragging = false;
            
            projectileQueue.Peek().gameObject.transform.SetParent(null);
            projectileQueue.Peek().gameObject.GetComponent<Rigidbody>().useGravity = true;
            projectileQueue.Peek().gameObject.GetComponent<Rigidbody>().AddForce(-direction * (mouseDistance * forceMultiplier), ForceMode.Impulse);
            projectileQueue.Peek().gameObject.GetComponent<Rigidbody>().excludeLayers = LayerMask.GetMask();
            projectileQueue.Peek().gameObject.GetComponent<ProjectileScript>().startTimer = true;
            
            projectileQueue.Dequeue();
        }
    }
    
    void GenerateSamples(Vector3 origin, Vector2 direction, float strength, float samples, float deltaT)
    {
        nodes.Clear();
        for (int i = 0; i < samples; i++)
        {
            Vector3 pos;
            pos.x = origin.x + direction.x * strength * i * deltaT;
            pos.y = origin.y + direction.y * strength * i * deltaT +
                    0.5f * Physics2D.gravity.y * i * i * deltaT * deltaT;
            pos.z = 0f;
            nodes.Add(pos);
        }
    }
}
