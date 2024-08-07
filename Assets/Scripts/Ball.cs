using UnityEngine;

public class Ball : MonoBehaviour
{
    // Config Params
    [SerializeField] PaddleMovement paddle1;
    [SerializeField] public float launchXMin = -3f;
    [SerializeField] public float launchXMax = 3f;
    [SerializeField] float launchY = 15f;
    [SerializeField] float constantSpeed = 1f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;


    bool hasStarted = false;

    //State
    Vector2 paddleToBallVector;

    //Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
        else
        {
            myRigidBody2D.velocity = constantSpeed * (myRigidBody2D.velocity.normalized);
        }
    }

    private void LaunchOnClick()
    {
        float launchX = UnityEngine.Random.Range(launchXMin, launchXMax);
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(launchX, launchY);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor),Random.Range(0f, randomFactor));
        if (hasStarted == true)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}