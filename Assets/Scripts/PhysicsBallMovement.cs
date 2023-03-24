using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBallMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private new Rigidbody rigidbody;

    [SerializeField] private AudioClip explosiveHit;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject explosionPrefab;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {   
        yield return new WaitForSeconds(Constants.StartDelay);
        ApplyRandomForce();
    }

    public void ApplyRandomForce()
    {
        Vector2 randomVector = Random.insideUnitCircle;
        Vector3 direction = new Vector3(randomVector.x, 0, randomVector.y);
        Vector3 velocity = direction * speed;
        rigidbody.AddForce(velocity, ForceMode.Impulse);
        GameObject explosion = Instantiate(explosionPrefab, transform.localPosition, transform.localRotation);
        audioSource.PlayOneShot(explosiveHit);
        Destroy(explosion, 0.3f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rigidbody.velocity += collision.gameObject.GetComponent<PaddleMovement>().PaddleVelocity.normalized;
        }
    }
}
