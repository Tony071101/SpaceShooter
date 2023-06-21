using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonRestart;
    [SerializeField] private GameObject buttonNext;
    private EnemySpawner enemySpawner;
    private Player player;
    private bool playerDead = false;
    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        player = FindObjectOfType<Player>();
    }

    public void GameOver()
    {
        if (playerDead == false)
        {
            playerDead = true;
            buttonRestart.SetActive(true);
            enemySpawner.StopSpawning();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        PlayerSpawner.Instance.PlayerSpawn();
    }

    public void NextLevel()
    {
        buttonNext.SetActive(true);
        enemySpawner.StopSpawning();
        player.enabled = false;
        Debug.Log("Next Level");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        PlayerSpawner.Instance.PlayerSpawn();
    }
}
