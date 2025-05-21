using UnityEngine;

public class TriggerParticleEffect2D : MonoBehaviour
{
    [Header("Assign the Particle System (optional)")]
    public ParticleSystem particleEffect;

    void Start()
    {
        if (particleEffect == null)
        {
            particleEffect = GetComponent<ParticleSystem>();
        }

        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && particleEffect != null && !particleEffect.isPlaying)
        {
            particleEffect.Play();
        }
    }
}
