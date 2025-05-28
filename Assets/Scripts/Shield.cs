using System.Collections;

using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject shield;
    [SerializeField] PlayerData playerData;
    [SerializeField] ParticleSystem smoke;
    private RaycastHit hit;
    public bool shieldIsActive;
    void Start()
    {
        if (playerData.shield)
        {
            ActivateShield();
        }
    }

    void Update()
    {
        ObstacleDetection();
    }

    private void ActivateShield()
    {
        shield.SetActive(true);
        shieldIsActive = true;
    }

    private void ObstacleDetection()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.55f))
        {
            if (shieldIsActive && hit.collider.gameObject.layer == 6)
            {
               /*  Debug.Log(hit.collider.gameObject.layer); */
                smoke.Play();
                Destroy(hit.collider.gameObject);
                DestroyShield();
            }
        }
    }
    public void DestroyShield()
    {
        shieldIsActive = false;
        shield.SetActive(false);
        StartCoroutine("ShieldRegeneration");
    }
    IEnumerator ShieldRegeneration()
    {
        yield return new WaitForSeconds(10);
        shield.SetActive(true);
        shieldIsActive = true;
    }

}
