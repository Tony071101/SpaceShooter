using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public static PlayerSelection Instance { get; private set; }
    private List<GameObject> charactersList = new List<GameObject>();
    private SliderStatController sliderStatController;
    private int selectedCharacter;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GameObject[] characterPrefabs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject characterPrefab in characterPrefabs)
        {
            
            charactersList.Add(characterPrefab);
        }
        

        selectedCharacter = 0;
        for (int i = 0; i < charactersList.Count; i++)
        {
            if (i == 0)
            {
                charactersList[i].SetActive(true);
                charactersList[i].GetComponent<Player>().enabled = false;
            }
            else
            {
                charactersList[i].SetActive(false);
            }
        }
        sliderStatController = FindObjectOfType<SliderStatController>();
        StatPlayer();
    }

    public void NextCharacter()
    {
        DisableCharacter();
        selectedCharacter = (selectedCharacter - 1 + charactersList.Count) % charactersList.Count;
        StatPlayer();
        EnableCharacter();
    }

    public void PreviousCharacter()
    {
        DisableCharacter();
        selectedCharacter = (selectedCharacter + 1) % charactersList.Count;
        StatPlayer();
        EnableCharacter();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        DontDestroyOnLoad(charactersList[selectedCharacter]);
        charactersList[selectedCharacter].GetComponent<Player>().enabled = true;
    }

    private void EnableCharacter()
    {
        charactersList[selectedCharacter].SetActive(true);
        charactersList[selectedCharacter].GetComponent<Player>().enabled = false;
    }

    private void DisableCharacter()
    {
        charactersList[selectedCharacter].SetActive(false);
        charactersList[selectedCharacter].GetComponent<Player>().enabled = false;
    }
    public GameObject SpawnCharacter()
    {
        return charactersList[selectedCharacter];
    }


    private void StatPlayer()
    {
        sliderStatController.SetHealth(charactersList[selectedCharacter].GetComponent<Player>().GetHealth());
        sliderStatController.SetSpeed(charactersList[selectedCharacter].GetComponent<Player>().GetSpeed());
        sliderStatController.SetAttack(charactersList[selectedCharacter].GetComponent<Player>().GetAttackDmg());
    }
}
