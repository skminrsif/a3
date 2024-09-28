// Djaleen Malabonga
// Student #3128901
using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private float _movementSpeed = 3.0f; // player movement
    private Vector2 _movement = new Vector2(); 
    private Rigidbody2D _rb;

    private Animator _anim;

    private void Start() { // intialize
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // if speed is near 0, no animation playing
        if (Mathf.Approximately(_movement.x, 0) && Mathf.Approximately(_movement.y, 0)) {
            _anim.SetBool("isWalking", false);

        } else { // else, walk animation starts playing
            _anim.SetBool("isWalking", true);

        }

        _anim.SetFloat("xDir", _movement.x);
        _anim.SetFloat("yDir", _movement.y);
        
    }

    void FixedUpdate() { // move velocity
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        _movement.Normalize();
        _rb.velocity = _movement * _movementSpeed;
    }

}