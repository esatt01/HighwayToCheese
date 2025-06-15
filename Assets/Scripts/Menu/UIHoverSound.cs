using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;
    private static AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null)
        {
            GameObject audioObj = GameObject.Find("UIAudio");
            if (audioObj != null)
            {
                audioSource = audioObj.GetComponent<AudioSource>();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}
