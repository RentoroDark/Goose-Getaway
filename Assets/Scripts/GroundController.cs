using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public Transform previousPlatform;
    public static event EventHandler<GameObject> OnRegeneration;
    public GameObject[] placeHolders;
    public GameObject[] platforTypes;
    private float speed = 4;
    public List<GameObject> obstacleBufer;
    [SerializeField] float acceleration = 0;
    



    
    
    void Update() 
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, -24), speed * Time.deltaTime);
        speed += acceleration * Time.deltaTime;
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Regenerate")
        OnRegeneration?.Invoke(this, gameObject);
    }
    
}
