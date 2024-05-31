using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] public PlayerShipType equippedShip;
    [SerializeField] public PlayerSaveData saveData;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public int shipCurrentHealth;
    [SerializeField] public int shipTotalHealth;
    [SerializeField] public bool isAlive;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip playerDeathSFX;
    [SerializeField] public float coinsGathered;
    [SerializeField] public float gemsGathered;
    [SerializeField] public SpriteRenderer shipAppearance;
    [SerializeField] public Sprite equippedAppearance;

    void Start()
    {
        if (PlayerPrefs.HasKey("currentShipEquiped"))
        {
            saveData.UpdateShipEquipped();
        }
        equippedShip = saveData.shipEquipped;
        playerAnimator.enabled = false;
        shipAppearance = GetComponent<SpriteRenderer>();
        equippedAppearance = equippedShip.shipAppearance;
        GetShipInfo();
        coinsGathered = 0;
        gemsGathered = 0;
        isAlive = true;
    }

    private void Update()
    {
        if(shipCurrentHealth <= 0)
        {
            playerAnimator.enabled = true;
            playerAnimator.Play("PlayerDeathAnimation");
        }
    }

    private void GetShipInfo()
    {
        shipAppearance.sprite = equippedAppearance;
        shipTotalHealth = equippedShip.shipBaseHealth * equippedShip.shipLevel;
        shipCurrentHealth = shipTotalHealth;
    }

    public void Death()
    {
        shipCurrentHealth = 0;
        isAlive = false;
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<GameManager>().EndGame();
    }

    public void PlayDeathExplosion()
    {
        playerAudioSource.PlayOneShot(playerDeathSFX);
    }
}
