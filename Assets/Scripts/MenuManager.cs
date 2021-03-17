using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI CoinText;

    // Start is called before the first frame update
    void Start()
    {
        CoinText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }
    public void TileSelect()
    {
        SceneManager.LoadScene("TileSelect");
    }
    public void BackSelect()
    {
        SceneManager.LoadScene("BackSelect");
    }
}
