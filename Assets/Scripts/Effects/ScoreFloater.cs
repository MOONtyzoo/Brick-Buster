using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreFloater : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro textMesh;

    void Start()
    {
        animator.Play("ScoreFloat");
    }

    public void OnAnimationFinished() {
        Destroy(gameObject);   
    }

    public void SetValue(int val) {
        textMesh.text = "+" + val;
    }
}
