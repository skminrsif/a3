// Djaleen Malabonga
// Student #3128901
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    [SerializeField] private float _pursuitSpeed = 1.4f; // if running towards player
    [SerializeField] private float _wanderSpeed = 0.8f; // wandering 
    private float _currentSpeed = 3; // base speed
    [SerializeField] private float _directionChangeInterval = 3; // interval for direction change
    [SerializeField] private bool followPlayer = true; // if following player
    private Coroutine _moveCoroutine; // move coroutine
    private Rigidbody2D _rb2d; // the gameobject with this script
    private Animator _anim; // animation
    private Transform _targetTransform = null; 
    private Vector3 _endPosition; // end position of character
    private float _currentAngle = 0; // current angle of movement

    private CircleCollider2D _circleCollider; // circle collider

    private bool _isBumping = false; // if character is bumping an obstacle
    void Start()
    {
        _anim = GetComponent<Animator>();
        _currentSpeed = _wanderSpeed; 
        _rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());

        _circleCollider = GetComponent<CircleCollider2D>();
    }

    // coroutine for wandering
    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);

            }
            _moveCoroutine = StartCoroutine(Move(_rb2d, _currentSpeed));
            yield return new WaitForSeconds(_directionChangeInterval);

        }
    }
    void ChooseNewEndpoint()
    {
        _currentAngle += Random.Range(0, 360); // choose angle from 0 to 360
        _currentAngle = Mathf.Repeat(_currentAngle, 360); // keeps the randomized angle around the limits of 0 to 360

        if (_isBumping) { // if bumping on an obstacle
            _endPosition += Vector3FromAngle(-_currentAngle); // make the angle of end position negative = bouncing towards the other direction (supposedly)
            

        } else {
            _endPosition += Vector3FromAngle(_currentAngle); // just a random endpoint

        }

    }
    Vector3 Vector3FromAngle(float inputAngleDegrees) // makes a vector3 out of the randomized angle from ChooseNewEndpoint
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians),
        Mathf.Sin(inputAngleRadians), 0);

    }

    // move routine - responsible for moving the character
    public IEnumerator Move(Rigidbody2D rbToMove, float speed)
    {
        float remainingDistance = (transform.position -
        _endPosition).sqrMagnitude;
        while (remainingDistance > float.Epsilon)
        {
            if (_targetTransform != null)
            {
                _endPosition = _targetTransform.position;

            }
            if (rbToMove != null)
            {
                _anim.SetBool("isWalking", true);
                Vector3 newPosition =
                Vector3.MoveTowards(rbToMove.position, _endPosition, speed *
                Time.deltaTime);
                _rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position -
                _endPosition).sqrMagnitude;

            }
            yield return new WaitForFixedUpdate();

        }
        _anim.SetBool("isWalking", false);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && followPlayer) // if player is within range of circle collider, follow player
        {
            _currentSpeed = _pursuitSpeed;
            _targetTransform = collider.gameObject.transform;
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);

            }

            _moveCoroutine = StartCoroutine(Move(_rb2d, _currentSpeed));

        } 
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) // if player is no longer within range of circle collider, wander around
        {
            _anim.SetBool("isWalking", false);
            _currentSpeed = _wanderSpeed;
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);

            }

            _targetTransform = null;
        } 
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<CompositeCollider2D>() && collision.gameObject.tag != "Enemy") {  // if character bumps into a collider that is not the player, choose new endpoint to go to
            _isBumping = true;

            StopCoroutine(WanderRoutine());

            StartCoroutine(WanderRoutine());
            

        }

    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<CompositeCollider2D>() && collision.gameObject.tag != "Enemy") {  // no longer bumping
            _isBumping = false;
        }
        
    }



    // debugging purposes
    void OnDrawGizmos()
    {
        if (_circleCollider != null) 
        {
            Gizmos.DrawWireSphere(transform.position, _circleCollider.radius);
            Debug.DrawLine(_rb2d.position, _endPosition, Color.red);
        }
    }

}