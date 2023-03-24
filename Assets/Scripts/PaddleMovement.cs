using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float maxSpeed = 10;

    private Vector3 velocity;
    public Vector3 PaddleVelocity => velocity;
    [SerializeField] private PhysicsBallMovement ball;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        velocity = new Vector3(horizontal, 0f, 0f) * maxSpeed;
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;

        if (newPosition.x < WallParameters.WallBounds.xMin)
        {
            newPosition.x = WallParameters.WallBounds.xMin;
            velocity.x = 0f;
        }
        else if (newPosition.x > WallParameters.WallBounds.xMax)
        {
            newPosition.x = WallParameters.WallBounds.xMax;
            velocity.x = 0f;
        }

        transform.localPosition = newPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball.ApplyRandomForce();
        }
    }
}
