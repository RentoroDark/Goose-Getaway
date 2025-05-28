using System.Runtime.Remoting.Proxies;
using UnityEngine;

public class FootPrintDisplacement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float acceleration;
    private float speed = 4;
    void Update()
    {
        rb.linearVelocity = Vector3.back * speed;
        speed += acceleration * Time.deltaTime;
    }
}
