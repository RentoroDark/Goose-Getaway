using System.Collections;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] GameOverScript gameOverScript;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource healSound;
    [SerializeField] Shield shield;
    [SerializeField] Shrink shrink;
    private Queue coinQueue = new ();
    private bool isPlaying = false;
    private double lastClipEndTime;

    void Start()
    {
        lastClipEndTime = AudioSettings.dspTime;
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("GameOver")) //fire
        {
            
            if (playerData.diesFromFire)
            {
                if (playerData.secondLife)
                {
                    gameOverScript.Respawn();
                }
                else
                {
                    gameOverScript.Death();
                }
                
            }
            else
            {
                playerHealth.TakeDamage();
                playerMovement.Boost(1);
            }
            
        }
        if (other.CompareTag("Coin"))
        {
            coinQueue.Enqueue(1);
            if (!isPlaying)
            {
                StartCoroutine("PlayCoinSound");
            }
            playerMoney.Add();
        }
        if (other.CompareTag("Sandwich"))
        {
            healSound.Play();
            playerHealth.RestoreHealth(1 * playerData.healingStrenght);
            playerMovement.Boost(0.5f);
        }   
        if (other.CompareTag("Trap"))
        {
            if (shield.shieldIsActive)
            {
                shield.DestroyShield();
            }
            else
            {
                playerHealth.TakeDamage();
            }
        }
        if (other.CompareTag("River"))
        {
            gameOverScript.Escape();
        }
        if (other.CompareTag("Mushroom"))
        {
            shrink.Begin();
        }
        if (other.CompareTag("Bounds"))
        {
            gameOverScript.Respawn();
        }
    }

    IEnumerator PlayCoinSound()
    {
        isPlaying = true;
        do
        {
            coinQueue.Dequeue();
            coinSound.Play();
            yield return new WaitForSeconds(0.1f);
        }while (coinQueue.Count > 0);
        isPlaying = false;
    }
}
