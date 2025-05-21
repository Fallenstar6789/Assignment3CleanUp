using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;

    public SpawnPoint playerSpawnPoint;
    private GameObject currentPlayer;

    // NEW: Generator repair tracking
    public int generatorStage = 0;

    void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Optional: Load generator stage from PlayerPrefs if needed
        // generatorStage = PlayerPrefs.GetInt("GeneratorStage", 0);
    }

    public void SetupScene()
    {
        playerSpawnPoint = FindObjectOfType<SpawnPoint>();

        if (playerSpawnPoint == null)
        {
            Debug.LogWarning("No SpawnPoint found in the scene!");
            return;
        }

        if (currentPlayer == null)
        {
            currentPlayer = playerSpawnPoint.SpawnObject();
            DontDestroyOnLoad(currentPlayer);
        }
        else
        {
            playerSpawnPoint.SpawnObject(currentPlayer); // Reposition existing player
        }
    }

    void Update()
    {

    }

    public void SpawnPlayer()
    {
        if (currentPlayer == null && playerSpawnPoint != null)
        {
            currentPlayer = playerSpawnPoint.SpawnObject();
            DontDestroyOnLoad(currentPlayer);

            SetCameraFollow(currentPlayer.transform);
        }
    }

    private void SetCameraFollow(Transform target)
    {
        Camera mainCam = Camera.main;
        if (mainCam == null) return;

        Camera_Lock cameraLock = mainCam.GetComponent<Camera_Lock>();
        if (cameraLock != null)
        {
            cameraLock.target = target;
        }
        else
        {
            Debug.LogWarning("Main Camera does not have Camera_Lock component.");
        }
    }

    // OPTIONAL: Public helper method
    public bool IsGeneratorFullyRepaired()
    {
        return generatorStage >= 3;
    }
}