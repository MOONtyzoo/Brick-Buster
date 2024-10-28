using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float moveSpeed;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        ProcessPlayerInput();
        PointAtMouseCursor();
        ConstrainToScreen();
    }

    private void ProcessPlayerInput() {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector3 movementVector = moveSpeed*horizontalAxis*Vector3.right*Time.deltaTime;
        transform.position += movementVector;
    }

    private void PointAtMouseCursor() {
        Vector2 dirToMousePos = GetMousePosition() - (Vector2)transform.position;
        transform.up = dirToMousePos;
    }

    private Vector2 GetMousePosition() {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
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
