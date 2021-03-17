using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int Score;
    public int Speed;
    public int Coins;

    public int LinesBeforeSpeed;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI HSText;
    public GameObject[] Pentominoes;
    public Sprite[] Tiles;
    public GameObject[] Backs;

    int LinesDone;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Speed = 1;
        Coins = PlayerPrefs.GetInt("Coins");
        LinesDone = 0;
        PMovement.TotalLines = 0;
        for (int i = 0; i < Pentominoes.Length; i++)
        {
            foreach (Transform children in Pentominoes[i].transform)
            {
                children.gameObject.GetComponent<SpriteRenderer>().sprite = Tiles[PlayerPrefs.GetInt("Tile")];
            }
        }

        for(int i = 0; i < Backs.Length; i++)
        {
            Backs[i].SetActive(false);
        }
        Backs[PlayerPrefs.GetInt("Back")].SetActive(true);

        PMovement.HoldBlock = 100;
        PMovement.HoldDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();
        CoinText.text = Coins.ToString();
        SpeedText.text = Speed.ToString();
        HSText.text = PlayerPrefs.GetInt("HS").ToString();

        PlayerPrefs.SetInt("Coins", Coins);

        if (PMovement.TotalLines >= LinesBeforeSpeed + LinesDone && Speed < 10)
        {
            Speed++;
            LinesDone += LinesBeforeSpeed;
        }

        for(int i = 0; i < Pentominoes.Length; i++)
        {
            foreach(Transform children in Pentominoes[i].transform)
            {
                children.gameObject.GetComponent<SpriteRenderer>().sprite = Tiles[PlayerPrefs.GetInt("Tile")];
            }
        }
    }
}
