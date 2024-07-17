using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    // This method will be triggered when another collider enters the trigger collider attached to the game object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the method to handle what happens when the player picks up this item
            OnPickup(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    // Virtual method to be overridden by subclasses to define specific behavior for different pickups
    protected virtual void OnPickup(GameObject player)
    {
        // Default behavior (if any) can be defined here or left empty for subclasses to fully define
        Debug.Log($"{this.GetType().Name} picked up by player.");
    }
}
