using System;
using System.Collections;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    internal void Begin()
    {
        StartCoroutine("StartShrink");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    IEnumerator StartShrink()
    {
        for (int i = 0; i < 10; ++i)
        {
            var lerpBufer = Mathf.Lerp(transform.localScale.x, 0.25f, 0.2f);
            transform.localScale = new Vector3(lerpBufer, lerpBufer, lerpBufer);
            yield return null;
        }
        yield return new WaitForSeconds(10);
        for (int i = 0; i < 10; ++i)
        {
            var lerpBufer = Mathf.Lerp(transform.localScale.x, 0.5f, 0.2f);
            transform.localScale = new Vector3(lerpBufer, lerpBufer, lerpBufer);
            yield return null;
        }
    }
    
}
