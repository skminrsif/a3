// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New HitPoints", menuName = "HitPoints", order = 52)] // new asset file type

public class HitPoints : ScriptableObject
{
    [SerializeField] private float _value;

    // getter and setter for hit point value
    public float Value {
        get {
            return _value;
        }

        set {
            _value = value;
        }
    }

    
}
