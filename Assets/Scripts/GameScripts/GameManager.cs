using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] public GameObject player;
    [SerializeField] public TextMeshProUGUI lifeText;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public GameObject spawner;
    [SerializeField] public bool gameIsActive;
    [SerializeField] public bool gameIsPaused;
    [SerializeField] public bool settingsActive;
    [SerializeField] public float playerScore;
    [SerializeField] public float scoreMultiplier;
    [SerializeField] public VerticalScrolling backgroundScroll;
    [SerializeField] public List<AudioSource> gameAudioSource;
    [Header("MenuScreens")]
    [SerializeField] public GameObject liveGameScreen;
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] public GameObject pauseScreen;
    [SerializeField] public GameObject settingsScreen;
    [Header("Game Texts")]
    [SerializeField] private float healthHolder;
    [SerializeField] private int scoreHolder;
    [SerializeField] public TextMeshProUGUI endScoreText;
    [SerializeField] public TextMeshProUGUI scoreTitleText;
    [SerializeField] public TextMeshProUGUI pauseScoreText;
    [SerializeField] public TextMeshProUGUI coinText;
    [SerializeField] public TextMeshProUGUI gemText;

    void Start()
    {
        StartGame();
        gameIsPaused = false;
        settingsActive = false;
    }

    void Update()
    {
        if (gameIsActive)
        {
            if (healthHolder != player.GetComponent<Player>().shipCurrentHealth)
            {
                UpdateLifePoints();
            }
            playerScore += Time.deltaTime * scoreMultiplier;
            scoreHolder = Mathf.RoundToInt(playerScore);
            scoreText.text = "SCORE: " + scoreHolder;
        }
    }

    public void UpdateLifePoints()
    {
        healthHolder = player.GetComponent<Player>().shipCurrentHealth;
        lifeText.text = "LIFE: " + player.GetComponent<Player>().shipCurrentHealth.ToString();
    }

    public void EndGame()
    {
        for(int x = 0; x < gameAudioSource.Count; x++)
        {
            gameAudioSource[x].Pause();
        }
        Time.timeScale = 0;
        spawner.GetComponent<Spawner>().isActive = false;
        liveGameScreen.SetActive(false);
        player.GetComponent<PlayerMovement>().playerLock = true;
        gameOverScreen.SetActive(true);
        gameIsActive = false;
        backgroundScroll.backgroundScrolling = false;
        if (scoreHolder > PlayerPrefs.GetInt("playerHighScoreData"))
        {
            scoreTitleText.text = "NEW HIGH SCORE!";
            endScoreText.text = scoreHolder.ToString();
            PlayerPrefs.SetInt("playerHighScoreData", scoreHolder);
        }
        else
        {
            scoreTitleText.text = "YOUR SCORE";
            endScoreText.text = scoreHolder.ToString();
        }
        SetUpPlayerCurrency();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        liveGameScreen.SetActive(true);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        playerScore = 0;
        gameIsActive = true;
        player.GetComponent<PlayerMovement>().playerLock = false;
        spawner.GetComponent<Spawner>().isActive = true;
        UpdateLifePoints();
    }

    public void PauseGame()
    {
        if (!gameIsPaused)
        {
            gameIsPaused = true;
            Time.timeScale = 0;
            liveGameScreen.SetActive(false);
            pauseScreen.SetActive(true);
            pauseScoreText.text = scoreHolder.ToString();
            for (int y = 0; y < gameAudioSource.Count; y++)
            {
                gameAudioSource[y].Pause();
            }
        }
        else
        {
            gameIsPaused = false;
            Time.timeScale = 1;
            liveGameScreen.SetActive(true);
            pauseScreen.SetActive(false);
            for (int z = 0; z < gameAudioSource.Count; z++)
            {
                gameAudioSource[z].UnPause();
            }
        }

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("MenuScene");
    }

    public void ShowGameSettings()
    {
        if (!settingsActive)
        {
            settingsActive = true;
            settingsScreen.SetActive(true);
            pauseScreen.SetActive(false);
        }
        else
        {
            settingsActive = false;
            settingsScreen.SetActive(false);
            pauseScreen.SetActive(true);
        }
    }

    private void SetUpPlayerCurrency()
    {
    coinText.text = player.GetComponent<Player>().coinsGathered.ToString();
    if (PlayerPrefs.HasKey("playerGoldData"))
        {
        PlayerPrefs.SetInt("playerGoldData", PlayerPrefs.GetInt("playerGoldData") + Mathf.RoundToInt(player.GetComponent<Player>().coinsGathered));
        }
    else
        {
        PlayerPrefs.SetInt("playerGoldData", Mathf.RoundToInt(player.GetComponent<Player>().coinsGathered));
        }

    gemText.text = player.GetComponent<Player>().gemsGathered.ToString();
    if (PlayerPrefs.HasKey("playerGemData"))
        {
        PlayerPrefs.SetInt("playerGemData", PlayerPrefs.GetInt("playerGemData") + Mathf.RoundToInt(player.GetComponent<Player>().gemsGathered));
        }
    else
        {
        PlayerPrefs.SetInt("playerGemData", Mathf.RoundToInt(player.GetComponent<Player>().gemsGathered));
        }
    }
}
