using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour {
    [SerializeField] private float spinX = 0, spinY = 0, spinZ = 0;

    private static int _acceleration = 1;

    public static int acceleration {
        get { return _acceleration; }
        set { _acceleration = value; }
    }

    void Update() {
        transform.Rotate(spinX * _acceleration, spinY * _acceleration, spinZ * _acceleration);
    }
}
