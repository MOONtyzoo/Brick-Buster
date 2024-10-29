using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float moveSpeed;

    public float maximumTurnAngle = 60;
    private float currentTurnAngle = 0;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        ProcessPlayerInput();
        RotateToMouseCursor();
        ConstrainToScreen();
    }

    private void ProcessPlayerInput() {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector3 movementVector = moveSpeed*horizontalAxis*Vector3.right*Time.deltaTime;
        transform.position += movementVector;
    }

    private void RotateToMouseCursor() {
        Vector2 dirToMousePos = GetMousePosition() - (Vector2)transform.position;
        float targetAngle = Vector2.SignedAngle(dirToMousePos, Vector2.up);
        SetTurnAngle(targetAngle);
    }

    private Vector2 GetMousePosition() {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }

    private void SetTurnAngle(float newTurnAngle) {
        currentTurnAngle = Math.Clamp(newTurnAngle, -maximumTurnAngle, maximumTurnAngle);
        Vector2 directionFromAngle = Quaternion.AngleAxis(currentTurnAngle, Vector3.back) * Vector2.up;
        transform.up = directionFromAngle;
    }

    private void ConstrainToScreen() {
        transform.position = SpriteTools.ConstrainSpriteToScreen(spriteRenderer);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    public void Reset() {
        gameObject.SetActive(true);
    }
}
