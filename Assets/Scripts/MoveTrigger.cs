using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    [SerializeField] TutorialCanvas tutorial;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorial.MoveTutorial();
        }
    }
}
