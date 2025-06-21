using UnityEngine;
using UnityEngine.UI;

public class SpxControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicSource;
    public AudioSource cheeseSpxSource;

    [Header("Sound Icons")]
    public Image soundIconImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private const string SpxPrefKey = "SpxVolume";
    private const string MutedPrefKey = "IssMuted";

    private bool isMuted = false;
    private float lastVolume = 1f;

    void Start()
    {
        float savedVolume = PlayerPrefs.HasKey(SpxPrefKey) ? PlayerPrefs.GetFloat(SpxPrefKey) : 0.3f;
        isMuted = PlayerPrefs.GetInt(MutedPrefKey, 0) == 1;

        lastVolume = savedVolume;
        volumeSlider.value = savedVolume;
        // Ýlk sesi ayarla
        musicSource.volume = isMuted ? 0f : volumeSlider.value;
        // Yeni kaynaðý da ayarla
        cheeseSpxSource.volume = musicSource.volume;

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
        bool muteNow = value <= 0.001f;
        isMuted = muteNow;
        float vol = muteNow ? 0f : value;

        // Her iki kaynaða uygula
        musicSource.volume = vol;
        cheeseSpxSource.volume = vol;


        PlayerPrefs.SetFloat(SpxPrefKey, value);
        PlayerPrefs.SetInt(MutedPrefKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        UpdateIcon();
    }


    void ToggleMute()
    {
        isMuted = !isMuted;
        float vol = isMuted ? 0f : lastVolume;

        musicSource.volume = vol;
        cheeseSpxSource.volume = vol;


        PlayerPrefs.SetInt(MutedPrefKey, isMuted ? 1 : 0);
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
