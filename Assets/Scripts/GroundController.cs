using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public Transform previousPlatform;
    public static event EventHandler<GameObject> OnRegeneration;
    public GameObject[] placeHolders;
    public GameObject[] platforTypes;
    
    public List<GameObject> obstacleBufer;
     
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Regenerate")
        OnRegeneration?.Invoke(this, gameObject);
    }
    
}
