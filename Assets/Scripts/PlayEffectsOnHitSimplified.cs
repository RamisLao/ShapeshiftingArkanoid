using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayEffectsOnHitSimplified : MonoBehaviour
{
    [SerializeField] private AudioClip wallHit;
    [SerializeField] private AudioClip bottomWallHit;
    private AudioSource audioSource;
    [SerializeField] private GameObject particlePrefab;

    [SerializeField] private ObjectShake cameraShake;
    [SerializeField] private float shakeIntensityWall = 0.5f;
    [SerializeField] private float shakeIntensityBottomWall = 0.5f;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject particles = Instantiate(particlePrefab, collision.contacts[0].point, Quaternion.identity);
        Destroy(particles, 0.3f);

        switch (collision.gameObject.tag)
        {
            case "Wall":
                audioSource.PlayOneShot(wallHit, Constants.WallHitVolume);
                cameraShake.Shake(shakeIntensityWall);
                return;
            case "Bottom Wall":
                audioSource.PlayOneShot(bottomWallHit, Constants.BottomWallHitVolume);
                cameraShake.Shake(shakeIntensityBottomWall);
                return;
            default:
                return;
        }
    }
}
