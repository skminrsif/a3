// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _ammoPrefab; // ammo
    private static List<GameObject> _ammoPool; // ammo pool of weapon
    [SerializeField] private int _poolSize = 7; // base pool size
    [SerializeField] private float _weaponVelocity = 2; // weapon velocity

    private bool _isFiring; // if player is firing
    private Camera _localCamera; // local camera
    private float _positiveSlope; // positive slope (if player is firing at a positive slope)
    private float _negativeSlope; // negative slope (if player is firing at a negative slope)
    private Animator _anim; // for weapon animation

    [SerializeField] private float _distance = 2.0f; // distance of weapon shot (capped)

    enum Quadrant { // direction quadrants
        East,
        South,
        West,
        North
    }

    void Start() {
        _anim = GetComponent<Animator>();
        _isFiring = false; // not firing
        _localCamera = Camera.main; // main camera

        // get the quadrant points of the camera
        Vector2 lowerLeft = _localCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = _localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = _localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = _localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        _positiveSlope = GetSlope(lowerLeft, upperRight); // establish positive slope
        _negativeSlope = GetSlope(upperLeft, lowerRight); // establish negative slope
    }

    void Update() {
        if (Input.GetMouseButton(0)) { // if left click
            _isFiring = true; // fire ammo
            FireAmmo();

        }

        UpdateState(); // update animation state

    }

    void Awake() {  
        if (_ammoPool == null) { // if no ammo
            _ammoPool = new List<GameObject>(); // make ammo
        }

        for (int i = 0; i < _poolSize; i++) { // instantiate ammo pool
            GameObject ammoObject = Instantiate(_ammoPrefab);
            ammoObject.SetActive(false);
            _ammoPool.Add(ammoObject);

        }
    }

    public GameObject SpawnAmmo(Vector3 location) { // spawn ammo at a location
        foreach(GameObject ammo in _ammoPool) { // for the amount of ammo in ammo pool
            if (ammo.activeSelf == false) { // if no ammo yet
                ammo.SetActive(true); // "spawn" ammo at a location
                ammo.transform.position = location;
                return ammo;

            }

        }
        
        return null;

    }

    void OnDestroy() { // no more ammo if dead
        _ammoPool = null;
    }

    private bool isWithinRange(Vector3 mousePosition) { // if mouse click is within a certain range, return true

        float playerMouseX = mousePosition.x - transform.position.x; // distance between player x and mouse click x
        float playerMouseY = mousePosition.y - transform.position.y; // distance between player y and mouse click y

        // if within range, return true
        if ((playerMouseX < _distance && playerMouseX > -_distance) && (playerMouseY < _distance && playerMouseY > -_distance)) {
            return true;
        }

        return false;
    }

    void FireAmmo() { // fire ammo at mouse click
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get mouse position when click happens
        GameObject ammo = SpawnAmmo(transform.position); // spawn ammo at player position
        if (ammo != null) { 
            Arc arcScript = ammo.GetComponent<Arc>(); 
            float travelDuration = 1.0f / _weaponVelocity; // travel duration of ammo

            if (isWithinRange(mousePosition)) { // if mouse click is within range
                StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration)); // start ammo travel coroutine

            } else { // if not in range
                Vector3 newPosition = mousePosition; // mouse position

                // get distance between click and player
                float playerMouseX = mousePosition.x - transform.position.x; 
                float playerMouseY = mousePosition.y - transform.position.y;

                // if calculated distance is more than the capped distance, make the x/y values the capped distance, based on the offending direction
                if (playerMouseX > _distance) { 
                    newPosition.x = _distance + transform.position.x;
    
                } else if (playerMouseX < -_distance) {
                    newPosition.x = -_distance + transform.position.x;

                }

                if (playerMouseY > _distance) {
                    newPosition.y = _distance + transform.position.y;
    
                } else if (playerMouseY < -_distance) {
                    newPosition.y = -_distance + transform.position.y;

                }
                

                // start the coroutine with the new position
                StartCoroutine(arcScript.TravelArc(newPosition, travelDuration));
            }
            
            

            

        }
    }

    // get slope
    float GetSlope(Vector2 pointOne, Vector2 pointTwo) {
        return (pointTwo.y - pointOne.y) / (pointTwo.x - pointOne.x);

    }

    // check if input position lies higher than the positive slope line
    bool HigherThanPositiveSlopeLine(Vector2 inputPosition) {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = _localCamera.ScreenToWorldPoint(inputPosition);
        float yIntercept = playerPosition.y - (_positiveSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (_positiveSlope * mousePosition.x);
        return inputIntercept > yIntercept;
    }

    // check if input position lies higher than the negative slope line
    bool HigherThanNegativeSlopeLine(Vector2 inputPosition) {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = _localCamera.ScreenToWorldPoint(inputPosition);
        float yIntercept = playerPosition.y - (_negativeSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (_negativeSlope * mousePosition.x);
        return inputIntercept > yIntercept;

    }


    // get the quadrant of the mouse position based on the player position (using slope lines)
    Quadrant GetQuadrant() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 playerPosition = transform.position;
        bool higherThanPositiveSlopeLine = HigherThanPositiveSlopeLine(Input.mousePosition);
        bool higherThanNegativeSlopeLine = HigherThanNegativeSlopeLine(Input.mousePosition);
        if (!higherThanPositiveSlopeLine && higherThanNegativeSlopeLine) {
            return Quadrant.East;

        } else if (!higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine) {
            return Quadrant.South;

        } else if (higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine) {
            return Quadrant.West;

        } else {
            return Quadrant.North;

        }

    }

    // update firing animation state based on the quadrant (ex. if firing north, play the animation that fires the weapon north)
    void UpdateState() {
        if (_isFiring) {
            float x = 0, y = 0;
            Quadrant quad = GetQuadrant();
            switch (quad) {
                case Quadrant.East:
                    x = 1.0f;
                    break;

                case Quadrant.South:
                    y = -1.0f;
                    break;
                
                case Quadrant.West:
                    x = -1.0f;
                    break;
                
                case Quadrant.North:
                    y = 1.0f;
                    break;

            }

            _anim.SetBool("isFiring", true);
            _anim.SetFloat("fireXDir", x);
            _anim.SetFloat("fireYDir", y);
            _isFiring = false;

        } else {
            _anim.SetBool("isFiring", false);

        }
    }



}
