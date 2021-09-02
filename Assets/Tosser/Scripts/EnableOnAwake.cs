using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnAwake : MonoBehaviour
{
    public bool usaAwake;
    public GameObject[] enableObjects;

    private void Awake()
    {
        if (!usaAwake)
            return;

        for (int i = 0; i < enableObjects.Length; i++)
        {
            enableObjects[i].SetActive(true);
        }
    }
}
