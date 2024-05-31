using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData : MonoBehaviour
{
    [Header("Player Save Data")]
    [SerializeField] public int playerGold;
    [SerializeField] public int playerGems;
    [SerializeField] public int currentShipActive;
    [SerializeField] public PlayerShipType ship1Data;
    [SerializeField] public PlayerShipType ship2Data;
    [SerializeField] public PlayerShipType ship3Data;
    [SerializeField] public PlayerShipType shipEquipped;
    [SerializeField] public int playerHighScore;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("playerGoldData") && PlayerPrefs.HasKey("playerGemsData"))
        {
            playerGold = PlayerPrefs.GetInt("playerGoldData");
            playerGold = PlayerPrefs.GetInt("playerGemsData");
        }
        else
        {
            UpdatePlayerCurrency();
        }

        if (PlayerPrefs.HasKey("currentShipEquipped"))
        {
            currentShipActive = PlayerPrefs.GetInt("currentShipEquipped");
            UpdateShipEquipped();
        }
        else
        {
            currentShipActive = 1;
            PlayerPrefs.SetInt("currentShipEquipped", currentShipActive);
            shipEquipped = ship1Data;
        }
    }

    public void UpdatePlayerCurrency()
    {
        PlayerPrefs.SetInt("playerGoldData", PlayerPrefs.GetInt("playerGoldData") + playerGold);
        PlayerPrefs.SetInt("playerGemsData", PlayerPrefs.GetInt("playerGemsData") + playerGems);
    }

    public void UpdatePlayerScore()
    {
        PlayerPrefs.SetInt("playerHighScoreData", playerHighScore);
    }

    public void UpdateShipEquipped()
    {
        switch (currentShipActive)
        {
            case 1:
                shipEquipped = ship1Data;
                break;
            case 2:
                shipEquipped = ship2Data;
                break;
            case 3:
                shipEquipped = ship3Data;
                break;
            default:
                shipEquipped = ship1Data;
                break;
        }
    }
}
