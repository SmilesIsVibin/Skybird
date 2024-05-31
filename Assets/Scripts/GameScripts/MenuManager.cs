using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Details")]
    [SerializeField] private PlayerSaveData playerSaveData;
    [SerializeField] private GameObject playerAppearance;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private TextMeshProUGUI playerCoins;
    [SerializeField] private TextMeshProUGUI playerGems;
    [SerializeField] private TextMeshProUGUI ship1Label;
    [SerializeField] private TextMeshProUGUI ship2Label;
    [SerializeField] private TextMeshProUGUI ship3Label;
    [SerializeField] private TextMeshProUGUI shieldLevel;
    [SerializeField] private TextMeshProUGUI shieldCostToUpgrade;
    [SerializeField] private bool ship1IsActive;
    [SerializeField] private bool ship2IsActive;
    [SerializeField] private bool ship3IsActive;
    [SerializeField] private int baseCost;
    [SerializeField] private int upgradeCost;
    [SerializeField] private int maxUpgradeLevel;

    private void Start()
    {
        SetupPlayerAppearance();
        startPanel.SetActive(true);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
        SetupShop();
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        startPanel.SetActive(false);
    }
    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
        startPanel.SetActive(false);
    }
    public void ExitShop()
    {
        shopPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetupShop()
    {
        playerCoins.text = PlayerPrefs.GetInt("playerGoldData").ToString();
        playerGems.text = PlayerPrefs.GetInt("playerGemData").ToString();
        upgradeCost = baseCost * PlayerPrefs.GetInt("shieldLevel");
        if (PlayerPrefs.HasKey("currentShipEquipped"))
        {
            switch (PlayerPrefs.GetInt("currentShipEquipped"))
            {
                case 1:
                    ship1IsActive = true;
                    ship2IsActive = false;
                    ship3IsActive = false;
                    ship1Label.text = "EQUIPPED";
                    ship2Label.text = "";
                    ship3Label.text = "";
                    break;
                case 2:
                    ship1IsActive = false;
                    ship2IsActive = true;
                    ship3IsActive = false;
                    ship1Label.text = "";
                    ship2Label.text = "EQUIPPED";
                    ship3Label.text = "";
                    break;
                case 3:
                    ship1IsActive = false;
                    ship2IsActive = false;
                    ship3IsActive = true;
                    ship1Label.text = "";
                    ship2Label.text = "";
                    ship3Label.text = "EQUIPPED";
                    break;
                default:
                    ship1IsActive = true;
                    ship2IsActive = false;
                    ship3IsActive = false;
                    ship1Label.text = "EQUIPPED";
                    ship2Label.text = "";
                    ship3Label.text = "";
                    break;
            }

            shieldLevel.text = PlayerPrefs.GetInt("shieldLevel").ToString();
            shieldCostToUpgrade.text = upgradeCost.ToString() + " COINS";
            if(PlayerPrefs.GetInt("shieldLevel") >= maxUpgradeLevel)
            {
                upgradeButton.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("currentShipEquipped", 1);
            ship1IsActive = true;
            ship2IsActive = false;
            ship3IsActive = false;
            ship1Label.text = "EQUIPPED";
            ship2Label.text = "";
            ship3Label.text = "";
        }
    }

    public void EquipShip1()
    {
        if (!ship1IsActive)
        {
            PlayerPrefs.SetInt("currentShipEquipped", 1);
            ship1IsActive = true;
            ship2IsActive = false;
            ship3IsActive = false;
            ship1Label.text = "EQUIPPED";
            ship2Label.text = "";
            ship3Label.text = "";
            playerSaveData.currentShipActive = PlayerPrefs.GetInt("currentShipEquipped");
            playerSaveData.UpdateShipEquipped();
            SetupPlayerAppearance();
        }
    }

    public void EquipShip2()
    {
        if (!ship2IsActive)
        {
            PlayerPrefs.SetInt("currentShipEquipped", 2);
            ship1IsActive = false;
            ship2IsActive = true;
            ship3IsActive = false;
            ship1Label.text = "";
            ship2Label.text = "EQUIPPED";
            ship3Label.text = "";
            playerSaveData.currentShipActive = PlayerPrefs.GetInt("currentShipEquipped");
            playerSaveData.UpdateShipEquipped();
            SetupPlayerAppearance();
        }
    }

    public void EquipShip3()
    {
        if (!ship3IsActive)
        {
            PlayerPrefs.SetInt("currentShipEquipped", 3);
            ship1IsActive = false;
            ship2IsActive = false;
            ship3IsActive = true;
            ship1Label.text = "";
            ship2Label.text = "";
            ship3Label.text = "EQUIPPED";
            playerSaveData.currentShipActive = PlayerPrefs.GetInt("currentShipEquipped");
            playerSaveData.UpdateShipEquipped();
            SetupPlayerAppearance();
        }
    }
    
    public void UpgradeShield()
    {
        if(PlayerPrefs.GetInt("playerGoldData") > upgradeCost && PlayerPrefs.GetInt("shieldLevel") < maxUpgradeLevel)
        {
            PlayerPrefs.SetInt("playerGoldData", PlayerPrefs.GetInt("playerGoldData") - upgradeCost);
            PlayerPrefs.SetInt("shieldLevel", PlayerPrefs.GetInt("shieldLevel") + 1);
            SetupShop();
        }
    }

    private void SetupPlayerAppearance()
    {
        playerAppearance.GetComponent<SpriteRenderer>().sprite = playerSaveData.shipEquipped.shipAppearance;
    }
}
