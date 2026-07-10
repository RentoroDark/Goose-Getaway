using System;
using System.Collections;
using UnityEngine;

public class FirePosition : MonoBehaviour
{
    RaycastHit hit;
    void Start()
    {
        StartCoroutine(SetPosition());
    }

    IEnumerator SetPosition()
    {
        yield return null;
        yield return null;
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width * 0.5f, 0)), out hit);
        
        transform.position = hit.point + new Vector3(0, 0.25f, -0.4f);
    }
}
