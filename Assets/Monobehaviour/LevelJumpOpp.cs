using UnityEngine;

public class LevelJumpOpp : MonoBehaviour
{
    [Tooltip("Name of the scene to load when the player enters.")]
    public string targetSceneName;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player")) 
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                SceneChange.instance.LoadScene(targetSceneName);
            }
            else
            {
                Debug.LogWarning("LevelJump: No target scene name set!");
            }
        }
    }
}
