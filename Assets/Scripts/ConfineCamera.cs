// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ConfineCamera : MonoBehaviour
{
  
    [SerializeField] private CinemachineConfiner _confiner; // get the confiner component from the vcam

    void OnTriggerEnter2D(Collider2D collider) {
        _confiner.m_BoundingShape2D = collider.gameObject.GetComponent<PolygonCollider2D>(); // set the new bounding shape as the colliding game object's polygon collider
        GameObject.Find("RPGGameManager").GetComponent<RPGGameManager>().SpawnBoss(); // spawn the boss when inside of new area


    }
}
