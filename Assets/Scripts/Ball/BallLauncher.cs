using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private Ball ball;

    private bool isBallLoaded = false;

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (isBallLoaded) {
                LaunchBall();
            }
        }
    }

    public void LaunchBall() {
        isBallLoaded = false;
        ball.transform.parent = null;
        ball.ActivatePhysics();
        ball.LaunchAtDesiredSpeedInDirection(transform.up);
    }

    public void LoadBall() {
        isBallLoaded = true;
        ball.transform.parent = transform;
        ball.transform.localPosition = Vector3.zero;
        ball.DeactivatePhysics();
    }
}
