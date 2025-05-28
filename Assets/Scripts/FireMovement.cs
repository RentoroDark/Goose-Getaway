using UnityEngine;

public class FireMovement : MonoBehaviour
{

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, -0.5f), 0.005f);
    }
}
