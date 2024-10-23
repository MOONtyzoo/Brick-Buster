using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        Cursor.visible = false;
    }

    private void FixedUpdate() {
        Move();
        ConstrainToScreen();
    }

    private void Move() {
        transform.position = new Vector3(GetMousePositionX(), transform.position.y, transform.position.z);
    }

    private float GetMousePositionX() {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition.x;
    }

    private void ConstrainToScreen() {
        transform.position = SpriteTools.ConstrainSpriteToScreen(spriteRenderer);
    }
}
