using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnEnemyPoint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyPrefab2;
    private GameObject enemyClone;
    private GameObject enemyClone2;
    private float positionXRight;
    private float positionXLeft;
    private float durationForDestroy = 7f;
    private float delayBetweenSpawns = 5f;
    private float durationDecreaseRate = 0.05f;
    private float minDuration = 1f;
    private bool isSpawning = true;

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (isSpawning)
        {
            positionXRight = Random.Range(0.5f, 2f);
            positionXLeft = -positionXRight;
            enemyClone = Instantiate(enemyPrefab, new Vector3(spawnEnemyPoint.position.x + positionXRight, spawnEnemyPoint.position.y, spawnEnemyPoint.position.z), Quaternion.identity);
            enemyClone2 = Instantiate(enemyPrefab2, new Vector3(spawnEnemyPoint.position.x + positionXLeft, spawnEnemyPoint.position.y, spawnEnemyPoint.position.z), Quaternion.identity);
            Destroy(enemyClone, durationForDestroy);
            Destroy(enemyClone2, durationForDestroy);

            yield return new WaitForSeconds(delayBetweenSpawns);

            delayBetweenSpawns -= durationDecreaseRate;
            delayBetweenSpawns = Mathf.Clamp(delayBetweenSpawns, minDuration, delayBetweenSpawns);
        }

    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {

    }
}
