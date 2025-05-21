using UnityEngine;
using TMPro;

public class ItemUIUpdater : MonoBehaviour
{
    public Item trackedItem;
    public TextMeshProUGUI quantityText;

    private void OnEnable()
    {
        if (trackedItem != null)
        {
            trackedItem.OnQuantityChanged += UpdateQuantityDisplay;
            UpdateQuantityDisplay(trackedItem.quantity); // Initial display
        }
    }

    private void OnDisable()
    {
        if (trackedItem != null)
        {
            trackedItem.OnQuantityChanged -= UpdateQuantityDisplay;
        }
    }

    private void UpdateQuantityDisplay(int quantity)
    {
        if (quantityText != null)
        {
            quantityText.text = $"Parts: {quantity}";
        }
    }
}
