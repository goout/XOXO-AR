using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerValue : MonoBehaviour {

    [SerializeField] private int value = 0;

    public int GetValue() {
        return value;
    }

}
