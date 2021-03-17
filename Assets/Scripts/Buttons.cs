using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    public bool Rotated =  false;
    public bool Dropped = false;
    public bool Lefted = false;
    public bool Righted = false;
    public bool downed = false;
    public bool Flipped = false;
    public bool holded = false;

    int current = 0;

    public GameObject PauseImage;
    public Button PauseButton;

    PMovement[] blocks;
    PMovement[] Tiles;

    PSpawn Spawn;
    void Start()
    {
        PauseImage.SetActive(false);
        Spawn = FindObjectOfType<PSpawn>();
    }


    public void Pause()
    {
        blocks = FindObjectsOfType<PMovement>();
        for (int i = 0; i < blocks.Length; i++)
        {
            if(blocks[i].enabled == true)
            {
                blocks[i].enabled = false;
                blocks[i].GetComponent<PMovement>().shadow.SetActive(false);
                current = i;
            }
        }
        PauseImage.SetActive(true);
        PauseButton.GetComponent<Image>().enabled = false;

        Tiles = FindObjectsOfType<PMovement>();
        for(int i = 0; i < Tiles.Length; i++)
        {
            SpriteRenderer[] sprites = Tiles[i].gameObject.GetComponentsInChildren<SpriteRenderer>();
            for(int j = 0; j < sprites.Length; j++)
            {
                sprites[j].enabled = false;
            }
        }
    }

    public void Resume()
    {
        blocks[current].enabled = true;
        blocks[current].GetComponent<PMovement>().shadow.SetActive(true);

        PauseImage.SetActive(false);
        PauseButton.GetComponent<Image>().enabled = true;

        for (int i = 0; i < Tiles.Length; i++)
        {
            SpriteRenderer[] sprites = Tiles[i].gameObject.GetComponentsInChildren<SpriteRenderer>();
            for (int j = 0; j < sprites.Length; j++)
            {
                sprites[j].enabled = true;
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    public void Rotate()
    {
        Rotated = true;
    }
    public void HardDrop()
    {
        Dropped = true;
    }
    public void Flip()
    {
        Flipped = true;
    }
    public void Hold()
    {
        holded = true;
    }
    public void UnHold()
    {
        holded = false;
    }

    public void MoveRight()
    {
        Righted = true;
    }
    public void RightFalse()
    {
        Righted = false;
    }

    public void MoveLeft()
    {
        Lefted = true;
    }
    public void LeftFalse()
    {
        Lefted = false;
    }

    public void SoftDrop()
    {
        downed = true;
    }
    public void DownFalse()
    {
        downed = false;
    }
}
