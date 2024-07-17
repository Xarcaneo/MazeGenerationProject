using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{
    protected override void OnPickup(GameObject player)
    {
        LevelManager.Instance.IncreaseScore();
    }
}
