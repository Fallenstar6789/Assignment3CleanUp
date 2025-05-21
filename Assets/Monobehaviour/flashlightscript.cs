using UnityEngine;
using UnityEngine.Rendering.Universal;

public class flashlightscript : MonoBehaviour
{
    [SerializeField] private Light2D lightFlash;
    [SerializeField] private Material shaderMaterial; // Assign the material with "_Depth" property
    [SerializeField] private float maxDepth = 1f;
    [SerializeField] private float minDepth = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {

            lightFlash.enabled = !lightFlash.enabled;

            if (!lightFlash.enabled)
            {
                shaderMaterial.SetFloat("_Depth", maxDepth);
            }
            else
            {
                shaderMaterial.SetFloat("_Depth", minDepth);
            }

        }
    }
}
