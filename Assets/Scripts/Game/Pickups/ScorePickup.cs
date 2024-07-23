using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a score pickup item that increases the player's score when collected.
/// Inherits from the abstract Pickup class.
/// </summary>
public class ScorePickup : Pickup
{
    /// <summary>
    /// Overrides the OnPickup method to increase the player's score when this item is collected.
    /// </summary>
    /// <param name="player">The player game object that picked up the item.</param>
    protected override void OnPickup(GameObject player)
    {
        // Increase the player's score via the LevelManager instance
        LevelManager.Instance.IncreaseScore();
    }
}
