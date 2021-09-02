using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public IEnumerator SlowdownTime(float duration, float timeScale)
    {
        if (timeScale == 0)
        {
            Debug.Log("trying to freeze time!");
            yield break;
        }
        Time.timeScale = timeScale;
        yield return new WaitForSeconds(duration);

    }
}
