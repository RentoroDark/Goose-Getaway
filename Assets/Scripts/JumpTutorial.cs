using UnityEngine;

public class JumpTutorial : MonoBehaviour
{
    [SerializeField] TutorialCanvas tutorial;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorial.JumpTutorial();
        }
    }
}
