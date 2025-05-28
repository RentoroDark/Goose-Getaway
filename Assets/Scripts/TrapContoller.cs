using UnityEngine;

public class TrapContoller : MonoBehaviour
{
    [SerializeField] Animator animator;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("playerTouched", true);    
        }
    }
}
