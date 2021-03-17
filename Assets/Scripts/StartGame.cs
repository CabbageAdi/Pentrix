using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject countdown;
    public Buttons buttons;
    public PSpawn spawn;

    void Start()
    {
        GameObject[] Backs = FindObjectOfType<GameManager>().Backs;
        for (int i = 0; i < Backs.Length; i++)
        {
            Backs[i].SetActive(false);
        }
        Backs[PlayerPrefs.GetInt("Back")].SetActive(true);
        if (PlayerPrefs.GetInt("Count") == 0)
        {
            countdown.SetActive(true);
            Invoke("DoIn", 3);
        }
        else
        {
            countdown.SetActive(false);
            buttons.enabled = true;
            spawn.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoIn()
    {
        countdown.SetActive(false);
        buttons.enabled = true;
        spawn.enabled = true;
    }
}
