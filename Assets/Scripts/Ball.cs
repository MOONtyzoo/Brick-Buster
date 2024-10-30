using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform paddleTransform;
    [SerializeField] private ParticleSystem particlePrefab;

    private Rigidbody physics;

    private bool isInPlay;
    private float startSpeed = 300;

    private void Awake() {
        physics = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        PlayBounceAnimation();
        if (collision.gameObject.tag == "Brick") {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            brick.Bust();
        }
    }

    private void PlayBounceAnimation() {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f*Vector3.one, 0.05f).SetEase(Ease.OutSine));
        sequence.Append(transform.DOScale(0.9f*Vector3.one, 0.1f).SetEase(Ease.InOutBounce));
        sequence.Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce));
    }

    private void Update() {
        if (isOkToLaunch() && Input.GetButtonDown("Fire1")) {
            Launch();
        }
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    public void Reset() {
        gameObject.SetActive(true);
        LoadBallOntoPaddle();
    }

    private bool isOkToLaunch() {
        if (isInPlay == false)
            return true;
        return false;
    }

    private void Launch() {
        isInPlay = true;
        transform.parent = null;
        physics.isKinematic = false;
        physics.AddForce(startSpeed*paddleTransform.up);
    }

    private void LoadBallOntoPaddle() {
        isInPlay = false;
        transform.parent = paddleTransform;
        transform.localPosition = new Vector3(0f, 0.5f, 0f);
        physics.isKinematic = true;
    }

    public void OnBallLost() {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
}
