using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicSource;

    [Header("Sound Icons")]
    public Image soundIconImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private const string VolumePrefKey = "MusicVolume";
    private const string MutePrefKey = "IsMuted";

    private bool isMuted = false;
    private float lastVolume = 1f;

    void Start()
    {
        // Load saved volume and mute state
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
        isMuted = PlayerPrefs.GetInt(MutePrefKey, 0) == 1;

        lastVolume = savedVolume;
        volumeSlider.value = savedVolume;

        if (isMuted)
        {
            musicSource.volume = 0f;
        }
        else
        {
            musicSource.volume = savedVolume;
        }

        UpdateIcon();

        volumeSlider.onValueChanged.AddListener(SetVolume);
        soundIconImage.GetComponent<Button>().onClick.AddListener(ToggleMute);
    }

    void SetVolume(float value)
    {
        lastVolume = value;

        if (value <= 0.001f)
        {
            isMuted = true;
            musicSource.volume = 0f;
        }
        else
        {
            isMuted = false;
            musicSource.volume = value;
        }

        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.SetInt(MutePrefKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        UpdateIcon();
    }


    void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            musicSource.volume = 0f;
        }
        else
        {
            musicSource.volume = lastVolume;
        }

        PlayerPrefs.SetInt(MutePrefKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (soundIconImage != null)
        {
            soundIconImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
        }
    }
}
