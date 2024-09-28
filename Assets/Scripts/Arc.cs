// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    // used to distinguish the travel arc of the weapon ammo (animation purposes)
    public IEnumerator TravelArc(Vector3 destination, float duration) {
        var startPosition = transform.position;
        var percentComplete = 0.0f;
        while (percentComplete < 1.0f) {
            percentComplete += Time.deltaTime / duration;
            var currentHeight = Mathf.Sin(Mathf.PI * percentComplete);
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) + Vector3.up * currentHeight;
            yield return null;

        }

        gameObject.SetActive(false);
        
    }
}
