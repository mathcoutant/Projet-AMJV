using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private float musicVolume;
    private float effectVolume;
    private float mainVolume;

    private void Start()
    {
        LoadValues();
    }
    public void ChangeMainVolume(float value)
    {
        mainVolume = value;
        PlayerPrefs.SetFloat("mainVolume", value);
        audioMixer.SetFloat("mainVolume", Mathf.Log10(mainVolume) * 20);
    }
    public void ChangeMusicVolume(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
    }
    public void ChangeEffectVolume(float value)
    {
        effectVolume = value;
        PlayerPrefs.SetFloat("effectVolume", value);
        audioMixer.SetFloat("effectVolume", Mathf.Log10(effectVolume) * 20);
    }
    //Save the setting values keep the same volumes between scenes
    public void SaveVolumeValues()
    {
        PlayerPrefs.SetFloat("mainVolume", mainVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("effectVolume", effectVolume);
    }
    public void LoadValues()
    {
        mainVolume = PlayerPrefs.GetFloat("mainVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        effectVolume = PlayerPrefs.GetFloat("effectVolume");
        audioMixer.SetFloat("mainVolume", Mathf.Log10(mainVolume) * 20);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("effectVolume", Mathf.Log10(effectVolume) * 20);
    }
}
