// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData", order = 51)] // new asset file type
public class ItemData : ScriptableObject
{
    // item types
    public enum ItemType {
        Coin,
        Health,

        MaxHealth,

        Milk,

        Apricot
    }

    [SerializeField] private string _objectName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _quantity;
    [SerializeField] private bool _isStackable;
    [SerializeField] private ItemType _itemType;

    // getters and setters

    public string ObjectName {
        get {
            return _objectName;
        }
    }

    public Sprite Sprite {
        get {
            return _sprite;
        }
    }

    public int Quantity {
        get {
            return _quantity;
        }

        set {
            _quantity = value;
        }
    }

    public bool IsStackable {
        get {
            return _isStackable;
        }
    }

    public ItemType Type {
        get {
            return _itemType;
        }
    }

    
}
