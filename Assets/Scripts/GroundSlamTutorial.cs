using UnityEngine;

public class GroundSlamTutorial : MonoBehaviour
{
    [SerializeField] TutorialCanvas tutorial;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorial.SlamTutorial();
        }
    }
}
