using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GameOverScript deathScript;
    [SerializeField] ParticleSystem vfx;
    private bool lowHealth = false;
    private Vignette vignette;
    public int health;
    public event Action healthChanged;
    void Start()
    {
        FindFirstObjectByType<Volume>().profile.TryGet(out vignette);
        
        health = playerData.GetMaxHealth();
        healthChanged?.Invoke();
        if (playerData.healthRegeneration > 0)
        {
            InvokeRepeating("Regeneration", 5, 5);
        }
    }
    void Update()
    {
        if (lowHealth)
        {
            vignette.intensity.value = 0.4f + (float)Math.Sin(Time.time * 3)*0.1f;
        }
    }
    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            deathScript.Death();
        }
        else if (health == 1)
        {
            vignette.active = true;
            lowHealth = true;
        }
        else
        {
            StartCoroutine("FlashVignette");
        }
        healthChanged?.Invoke();
    }
    public void Regeneration()
    {
        if (health < playerData.playerMaxHealth)
        {
            RestoreHealth(playerData.healthRegeneration);
        }
    }
    public void RestoreHealth(int amount)
    {
        vfx.Play();
        health += amount;
        if (health > 1)
        {
            lowHealth = false;
            vignette.active = false;
            vignette.intensity.value = 0;
        }
        healthChanged?.Invoke();
        
    }
    IEnumerator FlashVignette()
    {
        Debug.Log("vignette");
        vignette.active = true;
        for (float i = 0.3f; i < 0.4; i += 0.05f)
        {
            vignette.intensity.value = i;
            yield return null;
        }   
        for (float i = 0.4f; i > 0.3; i -= 0.05f)
        {
            vignette.intensity.value = i;
            yield return null;
        }      
        vignette.intensity.value = 0;
    }
}
