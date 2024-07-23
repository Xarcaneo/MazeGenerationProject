using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the visual focus state of a button by changing its sprite when the pointer enters and exits the button area.
/// </summary>
public class ButtonFocusHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// The default sprite for the button.
    /// </summary>
    [SerializeField, Tooltip("Default sprite for the button")]
    private Sprite defaultSprite;

    /// <summary>
    /// The sprite for the button when it is focused.
    /// </summary>
    [SerializeField, Tooltip("Focused sprite for the button")]
    private Sprite focusedSprite;

    /// <summary>
    /// The Image component of the button.
    /// </summary>
    [SerializeField, Tooltip("Image component for the button")]
    private Image buttonImage;

    /// <summary>
    /// Initializes the button image component and sets the default sprite.
    /// </summary>
    private void Awake()
    {
        if (buttonImage == null)
        {
            Debug.LogError("ButtonFocusHandler: No Image component found on the button.");
        }
        else
        {
            // Set the initial sprite to the default sprite
            buttonImage.sprite = defaultSprite;
        }
    }

    /// <summary>
    /// Changes the button sprite to the focused sprite when the pointer enters the button area.
    /// </summary>
    /// <param name="eventData">Current event data.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = focusedSprite;
        }
    }

    /// <summary>
    /// Reverts the button sprite to the default sprite when the pointer exits the button area.
    /// </summary>
    /// <param name="eventData">Current event data.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }
}
