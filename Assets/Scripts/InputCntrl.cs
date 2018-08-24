using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputCntrl : MonoBehaviour {

    private float holdTime = 1.5f;
    private float currentTime = 0;

    private const string CELL_TAG = "BoardCell";
    private const string WINNER_TEXT_TAG = "Restart";
    private const int MENU_SCENE_ID = 1;

    void Update() {

        GameObject tappedObject = StaticData.RaycastTag(CELL_TAG);

        if (tappedObject != null) {
            GameManager.instance.TapOnCell(tappedObject);
        } else {
            if (StaticData.RaycastTag(WINNER_TEXT_TAG) != null) {
                GameManager.instance.Restart();
            }
        }

        //exit to menu on tap & hold
        if (Input.touchCount > 0) {
            currentTime += Input.GetTouch(0).deltaTime;

            if (currentTime >= holdTime) {
                SceneManager.LoadScene(MENU_SCENE_ID);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                currentTime = 0;
            }
        }

    }
}