using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInterval;

    void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }

    public GameObject SpawnObject()
    {
        return SpawnObject(null);
    }

    public GameObject SpawnObject(GameObject existingObject)
    {
        GameObject spawned = null;

        if (existingObject != null)
        {
            // Move existing player to this spawn point
            existingObject.transform.position = transform.position;
            spawned = existingObject;
        }
        else if (prefabToSpawn != null)
        {
            // Spawn a new one if no existing player passed
            spawned = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }

        if (spawned != null)
        {
            // Main camera follow
            Camera_Lock cam = Camera.main?.GetComponent<Camera_Lock>();
            if (cam != null)
            {
                cam.target = spawned.transform;
            }

            // Mini-map camera follow
            GameObject miniMapCamObj = GameObject.Find("Mini_Map_Camera");
            if (miniMapCamObj != null)
            {
                Camera_Lock miniMapCam = miniMapCamObj.GetComponent<Camera_Lock>();
                if (miniMapCam != null)
                {
                    miniMapCam.target = spawned.transform;
                }
            }
        }

        return spawned;
    }

    void Update()
    {
        // Nothing here for now
    }
}
