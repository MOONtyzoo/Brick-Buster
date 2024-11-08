using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    new private Rigidbody rigidbody;

    private bool isActive = false;

    private float desiredSpeed = 5.0f;
    private float speedAdjustmentRate = 8.0f;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (isActive) {
            AdjustToDesiredVelocity();
        }
    }

    private void AdjustToDesiredVelocity() {
        float speed = GetSpeed();
        float speedDifference = Mathf.Abs(speed - desiredSpeed);
        
        float adjustmentDirection = (speed < desiredSpeed) ? 1f : -1f;
        float speedAdjustment = speedAdjustmentRate*adjustmentDirection*Time.deltaTime;

        if (Mathf.Abs(speedAdjustment) > speedDifference) {
            SetSpeed(desiredSpeed);
        } else {
            SetSpeed(GetSpeed() + speedAdjustment);
        }
    }

    public void Activate() {
        rigidbody.isKinematic = false;
        isActive = true;
    }

    public void Deactivate() {
        rigidbody.isKinematic = true;
        isActive = false;
    }

    public void SetVelocity(Vector2 newVelocity) {
        rigidbody.velocity = newVelocity;
    }

    public float GetDesiredSpeed() {
        return desiredSpeed;
    }

    public float GetSpeed() {
        return rigidbody.velocity.magnitude;
    }

    public void SetSpeed(float newSpeed) {
        Vector2 velocityDirection = rigidbody.velocity.normalized;

        rigidbody.velocity = newSpeed*velocityDirection;
    }

    public Vector2 GetDirection() {
        return rigidbody.velocity.normalized;
    }

    public void SetDirection(Vector2 newDirection) {
        float speed = rigidbody.velocity.magnitude;

        rigidbody.velocity = speed*newDirection;
    }
}
