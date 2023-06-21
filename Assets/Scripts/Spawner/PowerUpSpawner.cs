using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject speedBluePrefab;
    [SerializeField]
    private GameObject speedGreenPrefab;
    [SerializeField]
    private GameObject speedRedPrefab;
    [SerializeField]
    private GameObject dmgBluePrefab;
    [SerializeField]
    private GameObject dmgGreenPrefab;
    [SerializeField]
    private GameObject dmgRedPrefab;
    [SerializeField]
    private GameObject normalShield;
    [SerializeField]
    private GameObject averageShield;
    [SerializeField]
    private GameObject strongShield;
    public void SpawnPowerUp(Vector3 spawnPosition)
    {
        PowerUp.PowerUpType powerUpType = GetRandomPowerUpType();
        GameObject powerUpPrefab = GetPowerUpPrefab(powerUpType);
        if (powerUpPrefab != null)
        {
            GameObject powerUpInstance = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
            PowerUp powerUpComponent = powerUpInstance.GetComponent<PowerUp>();
            if (powerUpComponent != null)
            {
                powerUpComponent.powerUpType = powerUpType; // Set the power-up type
            }
        }
    }
    private PowerUp.PowerUpType GetRandomPowerUpType()
    {
        int randomIndex = UnityEngine.Random.Range(0, 9);
        return (PowerUp.PowerUpType)randomIndex;
    }

    private GameObject GetPowerUpPrefab(PowerUp.PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUp.PowerUpType.SpeedBlue:
                return speedBluePrefab;
            case PowerUp.PowerUpType.SpeedGreen:
                return speedGreenPrefab;
            case PowerUp.PowerUpType.SpeedRed:
                return speedRedPrefab;
            case PowerUp.PowerUpType.DmgBlue:
                return dmgBluePrefab;
            case PowerUp.PowerUpType.DmgGreen:
                return dmgGreenPrefab;
            case PowerUp.PowerUpType.DmgRed:
                return dmgRedPrefab;
            case PowerUp.PowerUpType.NormalShield:
                return normalShield;
            case PowerUp.PowerUpType.AverageShield:
                return averageShield;
            case PowerUp.PowerUpType.StrongShield:
                return strongShield;
            default:
                return null;
        }
    }
}
