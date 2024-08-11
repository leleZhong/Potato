using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> _BGMList;
    // public List<AudioSource> _effectList;

    public Settings _settings;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // SoundManager 객체를 파괴하지 않도록 설정
    }

    void Start()
    {
        _settings = FindObjectOfType<Settings>(); // 씬에서 Settings를 찾음
        Canvas canvas = FindObjectOfType<Canvas>(); // 씬에서 Canvas를 찾음

        if (_settings != null)
        {
            _settings._soundManager = this;
            
            _BGMList[0].playOnAwake = true; // BGM 추가
            _BGMList[0].loop = true;
            // _effectList[0].playOnAwake = false; // Effect 추가
            // _effectList[0].loop = false;
            InitializeSlider();
            LoadSettings();
        }
        else
        {
            Debug.LogWarning("Settings is not set.");
        }
    }

    void InitializeSlider()
    {
        if (_settings != null)
        {
            if (_settings._BGMSlider != null)
            {
                _settings._BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
            }
            if (_settings._effectSlider != null)
            {
                _settings._effectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
            }
        }
    }

    public void OnBGMVolumeChanged()
    {
        foreach (AudioSource audioSource in _BGMList)
        {
            audioSource.volume = _settings._BGMSlider.value;
        }

        Debug.Log("BGM Volume Changed: " + _settings._BGMSlider.value);

        SaveSettings();
    }

    public void OnEffectVolumeChanged()
    {
        // foreach (AudioSource audioSource in _effectList)
        // {
        //     audioSource.volume = _settings._effectSlider.value;
        // }

        SaveSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("BGMVolume", _settings._BGMSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", _settings._effectSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            _settings._BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        if (PlayerPrefs.HasKey("EffectVolume"))
        {
            _settings._effectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        }
    }
}
