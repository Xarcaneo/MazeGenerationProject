using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFocusHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Tooltip("Default sprite for the button")]
    private Sprite defaultSprite;

    [SerializeField, Tooltip("Focused sprite for the button")]
    private Sprite focusedSprite;

    [SerializeField, Tooltip("Image component for the button")]
    private Image buttonImage;

    private void Awake()
    {
        if (buttonImage == null)
        {
            Debug.LogError("ButtonFocusHandler: No Image component found on the button.");
        }
        else
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = focusedSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }
}
