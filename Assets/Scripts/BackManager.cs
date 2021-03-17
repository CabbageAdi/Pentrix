using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseBack(int Backnum)
    {
        PlayerPrefs.SetInt("Back", Backnum);
        SceneManager.LoadScene("Menu");
    }
}
