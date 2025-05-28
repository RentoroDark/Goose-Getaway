using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
using System.Runtime.Remoting.Messaging;
public class FooprintsController : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [Header("Settings")]
    public GameObject footprintPrefab;
    
    private ObjectPool<GameObject> footprintPool;
    public float footprintSpacing = 0.5f;
    public float footprintLifetime = 30f;
    public LayerMask groundLayers;
    public float distance;
    
    [Header("Footprint Offset")]
    public Vector3 positionOffset = new Vector3(0, 0.01f, 0);
    public Vector3 rotationOffset = new Vector3(90, 0, 0);
    
    private float lastFootprintTime;
    private bool isLeftFoot = false;
    private Queue<GameObject> activeFootprints = new ();

    void Start()
    {
        footprintPool = new 
        (() => 
            {return Instantiate(footprintPrefab);},
            footprint => {footprint.gameObject.SetActive(true);},
            footprint => {footprint.SetActive(false);},
            footprint => {Destroy(footprint.gameObject);},
            false, 10, 20
            
        );
        InvokeRepeating("CleanupOldFootprints", 5, 2);
    }

    void Update()
    {
        if (ShouldPlaceFootprint())
        {
            PlaceFootprint();
        }
        CleanupOldFootprints();
    }



    bool ShouldPlaceFootprint()
    {
        return  Time.time - lastFootprintTime > footprintSpacing;
    }

    void PlaceFootprint()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 
            out RaycastHit hit, distance, groundLayers))
        {
            
            // Alternate between left and right footprints
            Vector3 sideOffset = isLeftFoot ? 
                -transform.right * 0.1f : 
                transform.right * 0.1f;
            
            Quaternion rotation = Quaternion.Euler(rotationOffset)/* Quaternion.LookRotation(hit.normal) */;
            
            GameObject footprint = footprintPool.Get();
            footprint.transform.SetPositionAndRotation(hit.point + new Vector3(0, 0.1f, 0) + sideOffset, rotation);
            footprint.transform.parent = playerBody.transform;              
            
            // Add to cleanup list
            activeFootprints.Enqueue(footprint);
            
            // Set destroy timer
            
            // Update tracking
            lastFootprintTime = Time.time;
            isLeftFoot = !isLeftFoot;
        }
    }

    void CleanupOldFootprints()
    {
        footprintPool.Release(activeFootprints.Dequeue());
    }
}
