using UnityEngine;
using UnityEngine.UIElements;

public class CoinScript : MonoBehaviour
{
    
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 180 * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

}
