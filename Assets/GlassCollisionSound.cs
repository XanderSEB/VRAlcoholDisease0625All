using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlassCollisionSound : MonoBehaviour
{
    public AudioClip glassHitSound;
    public float minImpactVelocity = 1.0f;
    public float cooldown = 0.3f;

    private AudioSource audioSource;
    private float lastSoundTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Checke ob Kollision stark genug ist
        if (collision.relativeVelocity.magnitude > minImpactVelocity && Time.time - lastSoundTime > cooldown)
        {
            audioSource.PlayOneShot(glassHitSound);
            lastSoundTime = Time.time;
        }
    }
}