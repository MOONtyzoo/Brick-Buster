using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFloater : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator.Play("ScoreFloat");
    }

    public void OnAnimationFinished() {
        Destroy(gameObject);   
    }
}
