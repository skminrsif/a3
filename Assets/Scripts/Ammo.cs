// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _damageInflicted = 1; // ammo damge

    // when weapon shot hits enemy, damage enemy
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider is BoxCollider2D) {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            StartCoroutine(enemy.DamageCharacter(_damageInflicted, 0.0f));
            gameObject.SetActive(false);
        }
    }
}
