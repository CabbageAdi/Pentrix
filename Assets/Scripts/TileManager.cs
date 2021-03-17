using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TileSelect(int TileNum)
    {
        PlayerPrefs.SetInt("Tile", TileNum);
        SceneManager.LoadScene("Menu");
    }
}
