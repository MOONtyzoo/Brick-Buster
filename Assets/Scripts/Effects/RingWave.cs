using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingWave : MonoBehaviour
{
    private Renderer renderer;
    private Material material;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;

        float lifetime = material.GetFloat("_Lifetime");
        material.SetFloat("_StartTime", Time.time);

        Destroy(gameObject, lifetime);
    }
}
