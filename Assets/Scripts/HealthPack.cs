using UnityEngine;

public class HealthPack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 180 * Time.deltaTime, 0));     
    }
}
