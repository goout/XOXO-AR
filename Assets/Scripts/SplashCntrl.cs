using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashCntrl : MonoBehaviour {
    [SerializeField] private AudioClip splashSoundFx;
    [SerializeField] private int sceneID = 1;

    private AudioSource audioSource;
    private const string JAR_TAG = "Jar";

    void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        audioSource = GetComponent<AudioSource>();
    }

    public void SplashSound() {
        audioSource.PlayOneShot(splashSoundFx);
    }

    void LoadScene() {
        SceneManager.LoadScene(sceneID);
    }

    void Update() {
        if(StaticData.RaycastTag(JAR_TAG))
        LoadScene();
    }

}
