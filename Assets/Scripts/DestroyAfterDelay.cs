using System.Collections;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour {

    [SerializeField] private float delay = 8.0f;

    void Start() {
        Destroy(gameObject, delay);
    }

}
