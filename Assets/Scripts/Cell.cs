using System.Collections;
using UnityEngine;

public class Cell : MonoBehaviour {

    private BoxCollider cellColider;
    private MarkerValue playerMarker;
    private bool isMarked = false;
    private const int DEFAULT_CELL_VALUE = -10;

    void Start() {
        cellColider = GetComponent<BoxCollider>();
    }

    public void SetColliderEnabled(bool enabled) {
        cellColider.enabled = enabled;
    }

    public void Mark(GameObject markerPrefab) {
        if (!isMarked) {
            playerMarker = InstantiateMarker(markerPrefab).GetComponent<MarkerValue>();
            isMarked = true;
        }
    }

    public void RemoveMarker() {
        if (transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
            playerMarker = null;
            isMarked = false;
        }
    }

    private GameObject InstantiateMarker(GameObject playerMarker) {
        GameObject marker = Instantiate(playerMarker, Vector3.zero, playerMarker.transform.rotation);
        marker.transform.SetParent(transform, false);
        marker.transform.localPosition = Vector3.zero;
        return marker;
    }

    public bool IsMarked() {
        return isMarked;
    }

    public int getValue() {
        if (playerMarker != null) {
            return playerMarker.GetValue();
        } else {
            return DEFAULT_CELL_VALUE;
        }
    }

}
