using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance { get; private set; }
    [SerializeField] private Transform spawnPlayerPoint;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        PlayerSpawn();
    }


    public void PlayerSpawn()
    {
        GameObject playerPrefab = PlayerSelection.Instance.SpawnCharacter();
        GameObject clone = Instantiate(playerPrefab, spawnPlayerPoint.position, Quaternion.identity);

        playerPrefab.SetActive(false);
        clone.SetActive(true);
        //Destroy(playerPrefab);
    }

}
