using UnityEngine;

public class ExitTutorial : MonoBehaviour
{
    [SerializeField] TutorialCanvas tutorial;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorial.ExitTutorial();
        }
    }
}
