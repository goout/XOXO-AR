using System.Collections;
using UnityEngine;
using Wikitude;

public class ImageTrackerCntrl : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bynFx;
    [SerializeField] private AudioClip usdFx;
    [SerializeField] private AudioClip eurFx;
    [SerializeField] private TextMesh tooltip;

    private const string BYN = "5BYN";
    private const string EUR = "5EUR";
    private const string USD = "5USD";

    //on iamge lost
    public void tooltipActive() {
        tooltip.gameObject.SetActive(true);
    }

    //on iamge recognized
    public void tooltipInActive(ImageTarget it) {

        switch (it.Name) {

            case BYN:
                audioSource.PlayOneShot(bynFx);
                break;

            case USD:
                audioSource.PlayOneShot(usdFx);
                break;

            case EUR:
                audioSource.PlayOneShot(eurFx);
                break;

            default:
                audioSource.PlayOneShot(bynFx);
                break;

        }

        tooltip.gameObject.SetActive(false);
    }
}
