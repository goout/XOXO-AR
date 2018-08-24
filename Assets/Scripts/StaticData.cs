using System.Collections;
using UnityEngine;

public class StaticData : MonoBehaviour {

    private static bool aiPlayerMode = false;

    public static bool AiPlayerMode {
        get { return aiPlayerMode; }
        set { aiPlayerMode = value; }
    }

    public static GameObject RaycastTag(string tag) {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {

                GameObject gameObject = hit.transform.gameObject;

                if (gameObject.tag.Equals(tag)) {
                    return gameObject;
                }
            }
        }
        return null;
    }

}
