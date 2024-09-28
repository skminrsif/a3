// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected float _hitPoints; // hitpoints of enemy
    [SerializeField] private int _damageStrength; // damage strength of enemy
    private Coroutine _damageCoroutine; // damage coroutine


    // when enemy is getting damaged by player
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true) {
            StartCoroutine(FlickerCharacter());
            _hitPoints = _hitPoints - damage;

            if (_hitPoints <= float.Epsilon) {
                
                KillCharacter();
                break;

            } else if (interval > float.Epsilon) {
                yield return new WaitForSeconds(interval);
                break;

            } else {
                break;

            }
        }
    }


    // reset enemy
    public override void ResetCharacter()
    {
        _hitPoints = _startingHitPoints;

    }

    // reset enemy on enable
    private void OnEnable() {
        ResetCharacter();
    }

    // if enemy is getting collided with the player, hurt the player
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();
            if (_damageCoroutine == null) {
                _damageCoroutine = StartCoroutine(player.DamageCharacter(_damageStrength, 1.0f));

            }
        }
    }

    // if enemy is no longer damaging the player, stop the coroutine
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (_damageCoroutine != null) {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }
    }
    
}
