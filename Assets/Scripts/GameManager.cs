using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonRestart;
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private TextMeshProUGUI textHighScore;
    private EnemySpawner enemySpawner;
    private Player player;
    private UIManager uIManager;
    private bool playerDead = false;
    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        player = FindObjectOfType<Player>();
        uIManager = FindObjectOfType<UIManager>();
    }

    public void GameOver()
    {
        if (playerDead == false)
        {
            playerDead = true;
            buttonRestart.SetActive(true);
            textHighScore.enabled = true;
            textHighScore.text = ("Highest score: " + uIManager.GetTextScore().text);
            enemySpawner.StopSpawning();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        PlayerSpawner.Instance.PlayerSpawn();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        PlayerSpawner.Instance.PlayerSpawn();
    }
}
