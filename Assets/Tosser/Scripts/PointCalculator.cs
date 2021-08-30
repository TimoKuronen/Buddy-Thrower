using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.Core
{
    public class PointCalculator : MonoBehaviour
    {
        public static PointCalculator instance;
        [SerializeField] private int collectedCoins;
        [SerializeField] private int enemyPoints;
        [SerializeField] private int playerPoints;

        private void Awake()
        {
            instance = this;
        }

        public void CoinCollected(bool playerCollected)
        {
            if (playerCollected)
            {
                playerPoints++;
            }
            else
            {
                enemyPoints++;
            }

            collectedCoins++;
            CheckCollection();
        }

        void CheckCollection()
        {
            if (collectedCoins >= PrefabInstancer.instance.spawnCoinCount)
                EndRoundTrigger();

        }

        void EndRoundTrigger()
        {
            if (playerPoints > enemyPoints)
                Debug.Log("You win!");
            else Debug.Log("Enemies win!");
        }
    }
}