using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Toggle countintog;
    public TMP_Dropdown qualdrop;
    public AudioMixer mixer;

    void Start()
    {
        
    }
    void Update()
    {
        qualdrop.value = QualitySettings.GetQualityLevel();
        if(PlayerPrefs.GetInt("Count") == 1)
        {
            countintog.isOn = false;
        }
        else
        {
            countintog.isOn = true;
        }
    }

    public void Volume(float vol)
    {
        //mixer.SetFloat("Volume", vol);
    }

    public void Quality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void CountIn(bool value)
    {
        if(value == true)
        {
            PlayerPrefs.SetInt("Count", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Count", 1);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
