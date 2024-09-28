// Djaleen Malabonga
// Student #3128901
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character{
    [SerializeField] private HealthBar _healthBarPrefab; // health bar prefab
    private HealthBar _healthBar; // health bar
    [SerializeField] protected HitPoints _hitPoints; // player hitpoints

    [SerializeField] Inventory _inventoryPrefab; // inventory prefab
    private Inventory _inventory; // inventory
    public override void ResetCharacter() { // resets player
        _inventory = Instantiate(_inventoryPrefab);
        _healthBar = Instantiate(_healthBarPrefab);
        _healthBar.Character = this;
        _hitPoints.Value = _startingHitPoints;

    }

    private void OnEnable() { // resets player on enable
        ResetCharacter();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PickUp")) { // if player picks up item
            ItemData hitObject = collision.gameObject.GetComponent<Consumable>().Item; // get item data
            
            if (hitObject != null) {
                print("Hit: " + hitObject.ObjectName);
                bool shouldDisappear = false;
                switch (hitObject.Type) { // based on item data type
                    case ItemData.ItemType.Coin: 
                        shouldDisappear = _inventory.AddItem(hitObject); // add coin in item (for some weird reason, this throws a null reference exception error)
                        break;
                    
                    case ItemData.ItemType.Health:
                        shouldDisappear = AdjustHitPoints(hitObject.Quantity); // player heals back hp
                        break;

                    case ItemData.ItemType.MaxHealth:
                        AdjustMaxHealth(); // player is now immortal
                        shouldDisappear = true;
                        break;

                    case ItemData.ItemType.Apricot:
                        shouldDisappear = _inventory.AddItem(hitObject); // add apricot to inventory
                        break;

                    case ItemData.ItemType.Milk:
                        shouldDisappear = _inventory.AddItem(hitObject); // add milk to inventory
                        break;

                }

                if (shouldDisappear) {
                    collision.gameObject.SetActive(false); // set pickup to inactive when picked up
                }

            }
        } 

    }

    public bool AdjustHitPoints(int amount) { // heal player when player picks up health item
        if (_hitPoints.Value < _maxHitPoints) {
            _hitPoints.Value = _hitPoints.Value + amount;
            print("Adjusted HP by: " + amount + ". New Value: " + _hitPoints.Value);
            return true;

        }

        return false;
    }

    // caps the player health. the player is now immortal (kind of)
    public void AdjustMaxHealth() {
        _hitPoints.Value += 100;
    }

    public override IEnumerator DamageCharacter(int damage, float interval) // when player is getting damaged
    {
        while (true) {
            StartCoroutine(FlickerCharacter()); // flicker feedback when getting hit
            _hitPoints.Value = _hitPoints.Value - damage; // health reduced
            if (_hitPoints.Value <= float.Epsilon) { // when no more health left, kill player
                KillCharacter();
                break;

            } else if (interval > float.Epsilon) { // so the player doesn't get instantly obliterated
                yield return new WaitForSeconds(interval);

            } else {
                break;
            }
        }
    }

    public override void KillCharacter() // kill player, and destroy health bar and inventory objects
    {
        base.KillCharacter();
        Destroy(_healthBar.gameObject);
        Destroy(_inventory.gameObject);
    }

}
