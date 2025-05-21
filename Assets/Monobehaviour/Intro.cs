using UnityEngine;
using UnityEngine.UI;

public class IntroScreenManager : MonoBehaviour
{
    public GameObject introUI; // Assign the UI panel or image in Inspector
    private bool introActive = true;

    void Start()
    {
        if (introUI != null)
        {
            introUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game until Enter is pressed
        }
    }

    void Update()
    {
        if (introActive && Input.GetKeyDown(KeyCode.Return))
        {
            if (introUI != null)
            {
                introUI.SetActive(false);
                Time.timeScale = 1f; // Resume the game
                introActive = false;
            }
        }
    }
}
