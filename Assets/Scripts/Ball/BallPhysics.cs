using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    private Rigidbody physics;

    private bool isActive = false;

    private float desiredSpeed = 5.0f;
    private float speedAdjustmentRate = 8.0f;

    private void Awake() {
        physics = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (isActive) {
            AdjustToDesiredVelocity();
        }
    }

    public void Redirect(Vector2 newDirection) {
        SetBallDirection(newDirection);
    }

    public void AddSpeed(float extraSpeed) {
        SetBallSpeed(GetBallSpeed() + extraSpeed);
    }

    public void AdjustToDesiredVelocity() {
        float speed = GetBallSpeed();
        float speedDifference = Mathf.Abs(speed - desiredSpeed);
        
        float adjustmentDirection = (speed < desiredSpeed) ? 1f : -1f;
        float speedAdjustment = speedAdjustmentRate*adjustmentDirection*Time.deltaTime;

        if (Mathf.Abs(speedAdjustment) > speedDifference) {
            SetBallSpeed(desiredSpeed);
        } else {
            SetBallSpeed(GetBallSpeed() + speedAdjustment);
        }
    }

    public void SetVelocity(Vector2 newVelocity) {
        physics.velocity = newVelocity;
    }

    public float GetBallSpeed() {
        return physics.velocity.magnitude;
    }

    public void SetBallSpeed(float newSpeed) {
        Vector2 velocityDirection = physics.velocity.normalized;

        physics.velocity = newSpeed*velocityDirection;
    }

    public Vector2 GetBallDirection() {
        return physics.velocity.normalized;
    }

    public void SetBallDirection(Vector2 newDirection) {
        float speed = physics.velocity.magnitude;

        physics.velocity = speed*newDirection;
    }
}
