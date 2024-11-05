using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitbox : MonoBehaviour
{
    public float hitboxGrowthSpeed = 1.0f;
    public float hitboxEndRadius = 3.0f;

    private SphereCollider sphereCollider;

    void Awake() {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 0;
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Brick") {
            Brick brick = collider.gameObject.GetComponent<Brick>();
            if (!brick.isImmuneToExplosions) {
                brick.Bust();
            }
        }
    }

    void Update() {
        if (sphereCollider.radius < hitboxEndRadius) {
            sphereCollider.radius += hitboxGrowthSpeed*Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }
}
