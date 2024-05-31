using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPickupCurrency",menuName = "Pickup/Currency")]
public class PickupCurrency : PowerupEffect
{
    public float amountOfCurrency;
    public bool isGems;

    public override void ApplyEffect(GameObject targetObject)
    {
        if (isGems)
        {
            targetObject.GetComponent<Player>().gemsGathered += amountOfCurrency;
        }
        else
        {
            targetObject.GetComponent<Player>().coinsGathered += amountOfCurrency;
        }
    }
}
