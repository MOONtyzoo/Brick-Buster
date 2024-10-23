using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Ball : MonoBehaviour
{
    private Rigidbody physics;

    private bool isInPlay;

    private void Awake() {
        physics = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (isOkToLaunch()) {
            Launch();
        }
    }

    private bool isOkToLaunch() {
        if (isInPlay == false && Input.GetButtonDown("Fire1"))
            return true;
        return false;
    }

    private void Launch() {
        isInPlay = true;
        transform.parent = null;
        physics.isKinematic = false;
    }
}
