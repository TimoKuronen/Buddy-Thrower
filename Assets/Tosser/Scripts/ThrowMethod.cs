using System.Collections;
using UnityEngine;
using Tosser.Core;

public class ThrowMethod : MonoBehaviour
{
    public static ThrowMethod Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static IEnumerator Throwing(Vector3 targetPos, Vector3 startPos, float height, float speed, Transform transform, DragCharacter dragCharacter)
    {
        bool throwing = true;

        while (throwing)
        {
            float x0 = startPos.x;
            float x1 = targetPos.x;
            float dist = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
            float arc = height * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            transform.position = nextPos;

            // Do something when we reach the target
            float distance = Vector3.Distance(transform.position, targetPos);
            if (nextPos == targetPos || distance < 1)
            {
                Debug.Log("arrived");
                throwing = false;
                dragCharacter.DragCompleted(dragCharacter);
            }
            yield return null;
        }
    }

    public static IEnumerator Throwing(Vector3 targetTransform, float dragSpeed, bool returnRigidbody, float distanceLimit, bool disableWhenFinished, bool lookAt, Transform transform)
    {
        bool throwing = true;
        float distanceToTarget = Vector3.Distance(transform.position, targetTransform);
        while (throwing)
        {
            if (lookAt)
                transform.LookAt(targetTransform);

            // Calculate the angle in the arc
            float angle = Mathf.Min(1, Vector3.Distance(transform.position, targetTransform) / distanceToTarget) * 45;
            transform.rotation = transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);

            float currentDist = Vector3.Distance(transform.position, targetTransform);
            if (currentDist < distanceLimit)
            {
                throwing = false;
                if (disableWhenFinished)
                    transform.gameObject.SetActive(false);
            }
            else
                transform.Translate(Vector3.forward * Mathf.Min(dragSpeed * Time.deltaTime, currentDist));

            yield return null;
        }
        if (returnRigidbody)
            transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    public static IEnumerator Throwing(Vector3 targetPosition, Vector3 startPosition, float height, float duration, bool disableOnEnd, Transform transform)
    {
        bool throwing = true;
        float timer = 0;

        while (throwing)
        {
            timer += Time.deltaTime * duration;
            timer %= 5;
            transform.position = MathParabola.Parabola(startPosition, targetPosition, height, timer / duration);
            float dist = Vector3.Distance(transform.position, targetPosition);
            if (dist < 1)
            {
                throwing = false;
                transform.GetComponent<Rigidbody>().isKinematic = false;
                if (disableOnEnd)
                    transform.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
