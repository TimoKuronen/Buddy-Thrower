using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMethods : MonoBehaviour
{
    float health = default; // (default tekee sen aina siihe perusarvoo eli 0, false, tai empty mutta ei koskaa null) 
    private void Start()
    {
        StartCoroutine(TakeDamage(Die));
        StartCoroutine(TakeDamage(PlayParticles));
    }

    IEnumerator TakeDamage(Action done)
    {
        while (health > 0)
        {
            yield return new WaitForSeconds(1);
            health--;
        }
        done?.Invoke();
    }

    void Die()
    {
        // Something
    }

    void PlayParticles()
    {
        // Something else
    }
}
