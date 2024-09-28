// Djaleen Malabonga
// Student #3128901
using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {
    [SerializeField] protected float _maxHitPoints = 10;
    
    [SerializeField] protected float _startingHitPoints = 5;
    protected bool _killed = false; // if killed (used for boss kill quest)

    public CharacterCategory characterCategory;

    public enum CharacterCategory {
        Player,
        Enemy
    }

    public float MaxHitPoints { // getter and setter for max hit points
        get { 
            return _maxHitPoints; 
        } 
        set { 
            _maxHitPoints = value; 
        } 
    }

    public virtual void KillCharacter() { // destroy character object
        Destroy(gameObject);
    }

    public abstract void ResetCharacter(); // resets the character

    public abstract IEnumerator DamageCharacter(int damage, float interval); // damage character routine

    public virtual IEnumerator FlickerCharacter() { // hit feedback
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}