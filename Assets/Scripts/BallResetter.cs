using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball") {
            Game.Instance.LoseBall();
        }
    }
}
