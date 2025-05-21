using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RMInventory : MonoBehaviour
{
    public Item trackedItem;                     // ScriptableObject reference
    public TextMeshProUGUI quantityText;         // UI quantity text
    public Sprite partSprite;                    // Optional sprite (if using icon)

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        UpdateQuantityDisplay();
    }

    public bool AddItem(Item itemToAdd)
    {
        if (itemToAdd == trackedItem)
        {
            trackedItem.quantity += 1;
            UpdateQuantityDisplay();
            return true;
        }
        return false;
    }

    public bool RemoveItem(Item itemToRemove, int amount)
    {
        if (itemToRemove == trackedItem && trackedItem.quantity >= amount)
        {
            trackedItem.quantity -= amount;
            UpdateQuantityDisplay();
            return true;
        }
        return false;
    }

    public int GetQuantity(Item item)
    {
        return (item == trackedItem) ? trackedItem.quantity : 0;
    }

    public void UpdateQuantityDisplay()
    {
        if (quantityText != null && trackedItem != null)
        {
            quantityText.text = $"Parts: {trackedItem.quantity}";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-link UI elements after scene load
        var newText = GameObject.Find("QuantityText");
        if (newText != null)
        {
            quantityText = newText.GetComponent<TextMeshProUGUI>();
            UpdateQuantityDisplay();
        }
        else
        {
            Debug.LogWarning("QuantityText not found in scene: " + scene.name);
        }
    }
}