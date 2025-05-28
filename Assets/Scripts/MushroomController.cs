using UnityEngine;

public class MushroomController : MonoBehaviour
{
    
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
