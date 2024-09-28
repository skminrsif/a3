// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private GameObject _instance;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this.gameObject;
        _instance.SetActive(false); // set inactive so boss doesn't wander off its area
        
    }

    // Update is called once per frame
    void Update()
    {
        print(_hitPoints);
        if (_hitPoints <= 1) { // if boss is dead
            GameObject.Find("RPGGameManager").GetComponent<RPGGameManager>().CongratulatePlayer(); // kill boss quest complete
        }
    }



    
}
