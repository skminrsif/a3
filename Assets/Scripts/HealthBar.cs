// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HitPoints _hitPoints; // hit points
    [SerializeField] private Image _meterImage; // meter
    [SerializeField] private Text _hpText;

    private float _maxHitPoints; // max hit points of a character
    private Player _character;

    public Player Character { // getter and setter for character
        get {
            return _character;
        }

        set {
            _character = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _maxHitPoints = _character.MaxHitPoints; // establish max hit points
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_character != null) { // takes care of the health meter and the health points text
            _meterImage.fillAmount = _hitPoints.Value / _maxHitPoints;
            _hpText.text = "HP: " + (_meterImage.fillAmount * 100);

        }
    }
    
}
