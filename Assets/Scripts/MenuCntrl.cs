using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCntrl : MonoBehaviour {

    [SerializeField] private int sceneID = 2;
    [SerializeField] private AudioClip buttonPressed;

    private int _acceleration = 1;
    private AudioSource audioSource;

    private const string PLAYER_TAG = "One";
    private const string PLAYERS_TAG = "Two";

    public int acceleration {
        get {
            return _acceleration;
        }
    }

    void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        audioSource = GetComponent<AudioSource>();
        StaticData.AiPlayerMode = false;
    }

    void Update() {

        if (!MenuItemTap(StaticData.RaycastTag(PLAYER_TAG), true))
            MenuItemTap(StaticData.RaycastTag(PLAYERS_TAG), false);
    }

    private bool MenuItemTap(GameObject gameObject, bool aiPlayerMode) {

        if (gameObject != null) {
            StaticData.AiPlayerMode = aiPlayerMode;
            gameObject.GetComponent<TextMesh>().color = Color.red;
            StartCoroutine(PlaySoundThenLoad());
            return true;
        }
        return false;
    }

    IEnumerator PlaySoundThenLoad() {
        Spinner.acceleration = 5;
        audioSource.PlayOneShot(buttonPressed);
        yield return new WaitForSeconds(buttonPressed.length);
        SceneManager.LoadScene(sceneID);
    }


}