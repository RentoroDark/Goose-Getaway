using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] float acceleration = 0;
    private float speed = 4;
    void Update() 
    {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        speed += acceleration * Time.deltaTime;
        
    }
}
