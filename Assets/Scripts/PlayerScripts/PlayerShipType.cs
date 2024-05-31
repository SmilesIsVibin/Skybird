using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShip", menuName = "Create New Ship")]
public class PlayerShipType: ScriptableObject
{
    [Header("Player Ship Details")]
    [SerializeField] public string shipName;
    [SerializeField] public int shipLevel;
    [SerializeField] public Sprite shipAppearance;
    [SerializeField] public int shipBaseHealth;
}
