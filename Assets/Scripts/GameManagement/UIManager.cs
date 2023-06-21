using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private Image[] healths;
    private HealthSystem healthSystem;
    private int healthPlayer;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();
        healthPlayer = healthSystem.GetHeathSystem();
        HealthUI(healthPlayer);
    }

    public void IncreaseScore(int score)
    {
        textScore.text = score.ToString();
    }

    public void HealthUI(int healthPlayer)
    {
        this.healthPlayer = healthPlayer;
        for (int i = 0; i < healths.Length; i++)
        {
            healths[i].enabled = i < healthPlayer;
        }
    }
}
