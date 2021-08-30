using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tosser.Core;

namespace Tosser.Generics
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private GameObject visualObject;
        public int coinValue;

        public void PickUp(bool isPlayer)
        {
            StartCoroutine(nameof(TriggerPickup));
            PointCalculator.instance.CoinCollected(isPlayer);
        }

        IEnumerator TriggerPickup()
        {
            visualObject.SetActive(false);
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}
