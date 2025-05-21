using UnityEngine;
using TMPro;

public class Generator_Interaction : MonoBehaviour, IInteractable
{
    public RMInventory playerInventory; // ✅ Changed to match your script name
    public Item requiredPart;

    public int[] partsRequiredPerStage = { 8, 16, 32 };
    private int currentStage = 0;

    public TextMeshProUGUI uiText;
    public GameObject winnerScreen;

    public Door[] doorsToOpenPerStage; // Assign 2 doors for stage 0 and 1

    private bool fullyRepaired = false;

    [SerializeField] AudioSource effect;
    public AudioClip fix;



    public void PlayerInteraction()
    {
        if (playerInventory == null || requiredPart == null)
        {
            
            Debug.LogError("Generator_Interaction: Missing playerInventory or requiredPart reference.");
            return;
        }

        if (fullyRepaired)
        {
            uiText.text = "Generator fully repaired!";
            return;
        }

        int requiredCount = partsRequiredPerStage[currentStage];
        int count = playerInventory.GetQuantity(requiredPart); // ✅ uses simplified call

        if (count >= requiredCount)
        {
            effect.clip = fix;
            effect.Play();
            playerInventory.RemoveItem(requiredPart, requiredCount); // ✅ simplified removal
            uiText.text = $"Stage {currentStage} repair complete!";
            OpenDoorForStage(currentStage);

            currentStage++;

            if (RPGGameManager.sharedInstance != null)
            {
                RPGGameManager.sharedInstance.generatorStage = currentStage;
            }

            if (currentStage >= partsRequiredPerStage.Length)
            {
                fullyRepaired = true;
                ShowWinnerScreen();
            }
        }
        else
        {
            uiText.text = $"{requiredCount} parts are required to fix generator Stage {currentStage}.\n({count}/{requiredCount} collected)";
        }
    }

    public bool CanInteract()
    {
        return !fullyRepaired;
    }

    private void OpenDoorForStage(int stage)
    {
        if (stage < doorsToOpenPerStage.Length && doorsToOpenPerStage[stage] != null)
        {
            doorsToOpenPerStage[stage].Open();
        }
    }

    private void ShowWinnerScreen()
    {
        if (winnerScreen != null)
        {
            winnerScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Winner screen not assigned.");
        }
    }
}