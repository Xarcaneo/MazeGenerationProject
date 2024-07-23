using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for pickup items in the game. 
/// Handles the trigger interaction with the player and provides a virtual method for pickup behavior.
/// </summary>
public abstract class Pickup : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters the trigger collider attached to the game object.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Handle what happens when the player picks up this item
            OnPickup(other.gameObject);

            // Destroy the pickup object after it has been picked up
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Virtual method to handle the behavior when the player picks up this item.
    /// This method should be overridden by subclasses to define specific behavior for different pickups.
    /// </summary>
    /// <param name="player">The player game object that picked up the item.</param>
    protected virtual void OnPickup(GameObject player)
    {
        // Default behavior can be defined here or left empty for subclasses to fully define
        Debug.Log($"{this.GetType().Name} picked up by player.");
    }
}
