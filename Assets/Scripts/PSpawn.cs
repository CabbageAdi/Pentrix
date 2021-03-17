using UnityEngine;
using UnityEngine.UI;

public class PSpawn : MonoBehaviour
{
    public Image PreviewImage;
    public Image HoldImage;

    GameManager manager;

    int[] ToBeSpawned;
    int DoneInPack;
    int ITnuumber;
    public int CurrentP;

    GameObject PrevBlock;
    public GameObject HoldBlock;

    public GameObject[] Pentos;
    // Start is called before the first frame update
    void Start()
    {
        ITnuumber = 0;
        ToBeSpawned = new int[12];
        for (int i = 0; i < 12; i++)
        {
            ToBeSpawned[i] = i;
        }
        for (int i = 0; i < 12; i++)
        {
            int rand = Random.Range(0, 11);
            DoneInPack = ToBeSpawned[rand];
            ToBeSpawned[rand] = ToBeSpawned[i];
            ToBeSpawned[i] = DoneInPack;
        }
        SpawnP();

        manager = FindObjectOfType<GameManager>();

        foreach (Transform children in PrevBlock.transform)
        {
            children.gameObject.GetComponent<SpriteRenderer>().sprite = manager.Tiles[PlayerPrefs.GetInt("Tile")];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnP()
    {
        if(PrevBlock != null)
        {
            PrevBlock.SetActive(false);
        }
        Instantiate(Pentos[ToBeSpawned[ITnuumber]], transform.position, Quaternion.identity);
        CurrentP = ToBeSpawned[ITnuumber];
        if (ITnuumber == 11)
        {
            for (int i = 0; i < manager.Pentominoes.Length; i++)
            {
                int rand = Random.Range(0, 11);
                DoneInPack = ToBeSpawned[rand];
                ToBeSpawned[rand] = ToBeSpawned[i];
                ToBeSpawned[i] = DoneInPack;
            }
            ITnuumber = 0;
        }
        else
        {
            ITnuumber++;
        }
        PrevBlock = Instantiate(Pentos[ToBeSpawned[ITnuumber]], PreviewImage.transform.position, Quaternion.identity);
        PrevBlock.GetComponent<PMovement>().enabled = false;
        PrevBlock.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }
    public void SpawnNumber(int spawnnumber)
    {
        Instantiate(Pentos[spawnnumber], transform.position, Quaternion.identity);
        CurrentP = spawnnumber;
    }
}
