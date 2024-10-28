using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform paddleTransform;
    [SerializeField] private ParticleSystem particlePrefab;

    private Rigidbody physics;

    private bool isInPlay;
    [SerializeField] private float startSpeed = 100;

    private void Awake() {
        physics = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Brick") {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            brick.Bust();
        }
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
        transform.localPosition = new Vector3(0f, 0.2f, 0f);
        physics.isKinematic = true;
    }

    public void OnBallLost() {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
}
