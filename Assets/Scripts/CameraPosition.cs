using NUnit.Framework.Internal;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [Range(-2, 10)]
    public float testScale;
    private Vector3 camPos;
    public float screenRatio;
    float targetAspect;
    [SerializeField] bool limitFps;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (limitFps)
        {
            Application.targetFrameRate = 30;
        }
        screenRatio = (float)Screen.height / (float)Screen.width;
        transform.localPosition = new Vector3(0, 3.25f, -1.78f) - testScale * screenRatio * transform.forward;
        /* camPos = transform.localPosition; */
    }

    // Update is called once per frame
    
}
