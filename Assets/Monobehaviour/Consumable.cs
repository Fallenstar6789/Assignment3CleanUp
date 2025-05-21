using UnityEngine;
using UnityEngine.UIElements;

public class Consumable : MonoBehaviour
{
    public Item item;
    public GameObject labelText; // Assign this in the Inspector
    [SerializeField] AudioSource effect;
    public AudioClip collect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            effect.clip = collect;
            AudioSource.PlayClipAtPoint(collect, transform.position);

            // Add to inventory if needed
            RMInventory playerInventory = collision.GetComponent<RMInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(item);
            }

            // Hide or destroy the label
            if (labelText != null)
            {
                labelText.SetActive(false); // Or use Destroy(labelText);
            }

            // Destroy the item object
            Destroy(gameObject);
        }
    }
}

