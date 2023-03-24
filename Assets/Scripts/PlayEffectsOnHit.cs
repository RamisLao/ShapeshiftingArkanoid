using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffectsOnHit : MonoBehaviour
{
    [SerializeField] private AudioClip paddleHit;
    [SerializeField] private AudioClip wallHit;
    [SerializeField] private AudioClip bottomWallHit;
    [SerializeField] private AudioClip blockHit1;
    [SerializeField] private AudioClip blockHIt2;
    private AudioClip[] blockHits;
    private AudioSource audioSource;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private ScoreSystem scoreSystem;

    [SerializeField] private ObjectShake cameraShake;
    [SerializeField] private float shakeIntensityPaddle = 0.5f;
    [SerializeField] private float shakeIntensityWall = 0.5f;
    [SerializeField] private float shakeIntensityBottomWall = 2f;
    [SerializeField] private float shakeIntensityBlock = 0.7f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        blockHits = new AudioClip[] { blockHit1, blockHIt2 };
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject particles = Instantiate(particlePrefab, collision.contacts[0].point, Quaternion.identity);
        Destroy(particles, 0.3f);

        switch (collision.gameObject.tag)
        {
            case "Paddle":
                audioSource.PlayOneShot(paddleHit, Constants.PaddleHitVolume);
                cameraShake.Shake(shakeIntensityPaddle);
                return;
            case "Wall":
                audioSource.PlayOneShot(wallHit, Constants.WallHitVolume);
                cameraShake.Shake(shakeIntensityWall);
                return;
            case "Bottom Wall":
                audioSource.PlayOneShot(bottomWallHit, Constants.BottomWallHitVolume);
                cameraShake.Shake(shakeIntensityBottomWall);
                scoreSystem.SubtractScore(5);
                return;
            case "Block":
                audioSource.PlayOneShot(blockHits[Random.Range(0, blockHits.Length)], Constants.BlockHitVolume);
                cameraShake.Shake(shakeIntensityBlock);
                scoreSystem.AddScore(1);
                return;
            default:
                return;
        }
    }
}
