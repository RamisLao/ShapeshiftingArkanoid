using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 currentDirection;
    [SerializeField] private PaddleMovement paddleMovement;
    [SerializeField] private float paddleVelocityScalar;

    // Start is called before the first frame update
    private void Start()
    {
        currentDirection = Vector3.back;
    }

    // Update is called once per frame
    void Update()
    {
        currentDirection.Normalize();
        Vector3 velocity = currentDirection * speed;
        Vector3 displacement = velocity * Time.deltaTime;

        transform.localPosition += displacement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 sumOfNormals = Vector3.zero;
        for (int i = 0; i < collision.contactCount; i++)
        {
            sumOfNormals += collision.GetContact(i).normal;
        }

        sumOfNormals.Normalize();
        if (sumOfNormals == Vector3.forward || sumOfNormals == Vector3.back)
        {
            currentDirection.z = -currentDirection.z;
        }
        else if (sumOfNormals == Vector3.right || sumOfNormals == Vector3.left)
        {
            currentDirection.x = -currentDirection.x;
        }
        else
        {
            currentDirection.z = -currentDirection.z;
            currentDirection.x = -currentDirection.x;
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            currentDirection += paddleMovement.PaddleVelocity * paddleVelocityScalar;
        }
    }
}
