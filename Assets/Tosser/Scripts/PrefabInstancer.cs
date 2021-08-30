using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.Core
{
    public class PrefabInstancer : MonoBehaviour
    {
        public static PrefabInstancer instance;

        [SerializeField] private int spawnCoinCount;
        [SerializeField] private GameObject coinObject;
        [SerializeField] private GameObject specialCoinObject;
        [SerializeField] private List<Transform> coinSpawnPositions = new List<Transform>();

        [SerializeField] private int playerCount = 1;
        [SerializeField] private int currentPlayerCount;

        [SerializeField] private List<Transform> characterStartPositions = new List<Transform>();
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject playerAlly;
        [SerializeField] private GameObject enemyObject;
        private List<int> usedValues = new List<int>();

        public int GetCointCount => spawnCoinCount;

        private void Awake()
        {
            instance = this;
        }

        IEnumerator Start()
        {
            FillList(0, characterStartPositions.Count - 1, 4);
            SpawnPlayers();

            yield return new WaitForEndOfFrame();

            SpawnEnemies();
            usedValues.Clear();

            yield return new WaitForEndOfFrame();

            SpawnCoins();
        }

        void SpawnEnemies()
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    Instantiate(enemyObject, characterStartPositions[usedValues[i]].position, Quaternion.Euler(Vector3.zero));
                }
                else Instantiate(enemyObject, characterStartPositions[usedValues[i]].position 
                    + new Vector3(Random.Range(1,2), characterStartPositions[usedValues[i]].position.y, Random.Range(1, 2)), 
                    Quaternion.Euler(Vector3.zero));
            }
        }

        void SpawnPlayers()
        {
            for (int i = 2; i < 4; i++)
            {
                if (i == 2)
                {
                    currentPlayerCount++;
                    Instantiate(playerObject, characterStartPositions[usedValues[i]].position, Quaternion.Euler(Vector3.zero));
                }
                else Instantiate(playerAlly, characterStartPositions[usedValues[i]].position
                    + new Vector3(Random.Range(1, 2), characterStartPositions[usedValues[i]].position.y, Random.Range(1, 2)),
                    Quaternion.Euler(Vector3.zero));
            }           
        }

        void FillList(int min, int max, int loopCount)
        {
            for (int i = 0; i < loopCount; i++)
            {
                int val = Random.Range(min, max);
                while (usedValues.Contains(val))
                {
                    val = Random.Range(min, max);
                }
                usedValues.Add(val);
            }
        }

        void SpawnCoins()
        {
            FillList(0, coinSpawnPositions.Count - 1, spawnCoinCount);

            for (int i = 0; i < spawnCoinCount; i++)
            {
                Instantiate(coinObject, coinSpawnPositions[usedValues[i]].position, Quaternion.Euler(Vector3.zero));
            }
            usedValues.Clear();
        }
    }
}