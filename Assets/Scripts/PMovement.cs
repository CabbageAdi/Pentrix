using UnityEngine;
using UnityEngine.SceneManagement;

public class PMovement : MonoBehaviour
{
    float falltime;
    float previoustime;
    int LinesCleared;
    public static int TotalLines;

    int minDown;
    int minLeft;
    int minRight;

    public static Transform[,] grid = new Transform[25, 42];

    public static int HoldBlock = 100;
    float SideTime;
    public static bool HoldDone = false;

    PSpawn PentSp;
    GameManager Manager;
    Buttons ButtonS;

    public GameObject shadow;
    void Start()
    {
        Manager = FindObjectOfType<GameManager>();
        ButtonS = FindObjectOfType<Buttons>();

        minDown = 0;
        minLeft = 3;
        minRight = 14;

        LinesCleared = 0;
        PentSp = FindObjectOfType<PSpawn>();

        shadow = Instantiate(gameObject);
        Destroy(shadow.GetComponent<PMovement>());
    }

    // Update is called once per frame
    void Update()
    {
        falltime = 0.5f - (Manager.Speed * 0.04f);
        foreach (Transform children in transform)
        {
            children.GetComponent<SpriteRenderer>().sprite = Manager.GetComponent<GameManager>().Tiles[PlayerPrefs.GetInt("Tile")];
        }
        if ((Input.GetKey(KeyCode.LeftArrow) && SideTime + 0.09f <= Time.time) || (ButtonS.Lefted == true && SideTime + 0.09f <= Time.time))
        {
            SideTime = Time.time;
            transform.position -= new Vector3(1, 0, 0);
            if (!CanMove())
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        if ((Input.GetKey(KeyCode.RightArrow) && SideTime + 0.09f <= Time.time) || (ButtonS.Righted == true && SideTime + 0.09f <= Time.time))
        {
            SideTime = Time.time;
            transform.position += new Vector3(1, 0, 0);
            if (!CanMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || ButtonS.Rotated == true)
        {
            int HasMovedLeft = 0;
            int HasMovedRight = 0;
            transform.Rotate(0, 0, 90);
            if (!CanMoveDown())
            {
                transform.Rotate(0, 0, -90);
            }
            if (!CanMoveLeft())
            {
                while (!CanMoveLeft())
                {
                    if (NotGrid())
                    {
                        transform.position += new Vector3(1, 0, 0);
                        HasMovedRight++;
                    }
                    if (!NotGrid())
                    {
                        for(int i = 1; i <= HasMovedRight; i++)
                        {
                            transform.position -= new Vector3(1, 0, 0);
                        }
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
            if (!CanMoveRight())
            {
                while (!CanMoveRight())
                {
                    if (NotGrid())
                    {
                        transform.position -= new Vector3(1, 0, 0);
                        HasMovedLeft++;
                    }
                    if (!NotGrid())
                    {
                        for (int i = 1; i <= HasMovedLeft; i++)
                        {
                            transform.position += new Vector3(1, 0, 0);
                        }
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
            ButtonS.Rotated = false;
        }
        if (Input.GetKeyDown(KeyCode.F) || ButtonS.Flipped == true)
        {
            transform.Rotate(180, 0, 0);
            if (!CanMove())
            {
                transform.Rotate(-180, 0, 0);
            }
            ButtonS.Flipped = false;
        }
        if((Time.time - previoustime > (Input.GetKey(KeyCode.DownArrow) ? falltime/15 : falltime)) || (Time.time - previoustime > (ButtonS.downed == true ? falltime / 15 : falltime)))
        {
            transform.position -= new Vector3(0, 1, 0);
            if (!CanMove())
            {
                transform.position += new Vector3(0, 1, 0);
                float time = Time.time;
                while(time + 0.5 <= Time.time)
                {

                }
                AddToGrid();
                this.enabled = false;
                CheckForLine();
                PentSp.SpawnP();
            }
            previoustime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space) || ButtonS.Dropped == true)
        {
            while (CanMove())
            {
                transform.position -= new Vector3(0, 1, 0);
            }
            if (!CanMove())
            { 
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                this.enabled = false;
                CheckForLine();
                PentSp.SpawnP();
            }
            ButtonS.Dropped = false;
        }

        if ((ButtonS.holded == true || Input.GetKeyDown(KeyCode.C)) && HoldDone == false)
        {
            if(HoldBlock > Manager.Pentominoes.Length)
            {
                HoldBlock = PentSp.CurrentP;
                PentSp.HoldBlock = Instantiate(Manager.Pentominoes[HoldBlock], PentSp.HoldImage.transform.position, Quaternion.identity);
                PentSp.HoldBlock.GetComponent<PMovement>().enabled = false;
                PentSp.HoldBlock.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                this.enabled = false;
                gameObject.SetActive(false);
                PentSp.SpawnP();
            }
            else if(HoldBlock <= Manager.Pentominoes.Length)
            {
                PentSp.HoldBlock.SetActive(false);
                int previous = HoldBlock;
                HoldBlock = PentSp.CurrentP;
                PentSp.HoldBlock = Instantiate(Manager.Pentominoes[HoldBlock], PentSp.HoldImage.transform.position, Quaternion.identity);
                PentSp.HoldBlock.GetComponent<PMovement>().enabled = false;
                PentSp.HoldBlock.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                PentSp.SpawnNumber(previous);
                this.enabled = false;
                gameObject.SetActive(false);
            }
            HoldDone = true;
            Destroy(shadow);
        }
        for(int i = 0; i < 12; i++)
        {
            if(grid[i, 31] != null)
            {
                LoseGame();
            }
        }
        shadow.transform.rotation = transform.rotation;
        shadow.transform.position = transform.position;
        while (ShadowCanMove())
        {
            shadow.transform.position -= new Vector3(0, 1, 0);
        }
        if (!ShadowCanMove())
        {
            shadow.transform.position += new Vector3(0, 1, 0);
        }
        SpriteRenderer[] thing = shadow.GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i <= 4; i++)
        {
            thing[i].color = new Color(thing[i].color.r, thing[i].color.g, thing[i].color.b, 0.7f);
        }

        if (Manager.Score > PlayerPrefs.GetInt("HS"))
        {
            PlayerPrefs.SetInt("HS", Manager.Score);
        }

        foreach(Transform children in shadow.transform)
        {
            children.GetComponent<SpriteRenderer>().sprite = Manager.Tiles[PlayerPrefs.GetInt("Tile")];
        }
    }
    void LoseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if(Manager.Score > PlayerPrefs.GetInt("HS"))
        {
            PlayerPrefs.SetInt("HS", Manager.Score);
        }
    }
    bool CanMove()
    {
        foreach(Transform children in transform)
        {
            int Roundedx = Mathf.RoundToInt(children.position.x);
            int Roundedy = Mathf.RoundToInt(children.position.y);
            if (Roundedy < minDown || Roundedx < minLeft || Roundedx > minRight)
            {
                return false;
            }
            if(grid[Roundedx, Roundedy] != null)
            {
                return false;
            }
        }
        return true;
    }
    bool CanMoveRight()
    {
        foreach (Transform children in transform)
        {
            int Roundedx = Mathf.RoundToInt(children.position.x);
            int Roundedy = Mathf.RoundToInt(children.position.y);
            if (Roundedx > minRight)
            {
                return false;
            }
        }
        return true;
    }
    bool CanMoveLeft()
    {
        foreach (Transform children in transform)
        {
            int Roundedx = Mathf.RoundToInt(children.position.x);
            int Roundedy = Mathf.RoundToInt(children.position.y);
            if (Roundedx < minLeft)
            {
                return false;
            }
        }
        return true;
    }
    bool CanMoveDown()
    {
        foreach (Transform children in transform)
        {
            int Roundedx = Mathf.RoundToInt(children.position.x);
            int Roundedy = Mathf.RoundToInt(children.position.y);
            if (Roundedy < minDown)
            {
                return false;
            }
            if(grid[Roundedx, Roundedy] != null)
            {
                return false;
            }
        }
        return true;
    }
    bool NotGrid()
    {
        foreach (Transform children in transform)
        {
            int Roundedx = Mathf.RoundToInt(children.position.x);
            int Roundedy = Mathf.RoundToInt(children.position.y);
            if (grid[Roundedx, Roundedy] != null)
            {
                return false;
            }
        }
        return true;
    }
    bool ShadowCanMove()
    {
        foreach (Transform children in shadow.transform)
        {
            int Roundedx = Mathf.RoundToInt(children.position.x);
            int Roundedy = Mathf.RoundToInt(children.position.y);
            if (Roundedy < minDown)
            {
                return false;
            }
            if (grid[Roundedx, Roundedy] != null)
            {
                return false;
            }
        }
        return true;
    }
    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int Roundedx = Mathf.RoundToInt(children.position.x);
            int Roundedy = Mathf.RoundToInt(children.position.y);

            grid[Roundedx, Roundedy] = children;
        }
        Manager.Score += 10;
        HoldDone = false;
        Destroy(shadow);
    }

    void CheckForLine()
    {
        for (int i = 35; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DestroyLine(i);
                DropBlock(i);
                Manager.Score += 100;
                Manager.Coins += 1;
                LinesCleared += 1;
            }
        }
        if(LinesCleared == 5)
        {
            Manager.Coins += 5;
            Manager.Score += 100;
        }
        TotalLines += LinesCleared;
        LinesCleared = 0;
    }

    bool HasLine(int i)
    {
        for (int j = minLeft; j <= minRight; j++)
        {
            if(grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }
    void DestroyLine(int i)
    {
        GameObject ToDestroy;
        for(int j = 0; j < minRight; j++)
        {
          if (grid[j, i] != null)
          {
                ToDestroy = grid[j, i].gameObject;
                grid[j, i] = null;
                Destroy(ToDestroy);
          }
        }             
    }
    void DropBlock(int i)
    {
        for(int y = i; y < 35; y++)
        {
            for(int j = 0; j < minRight; j++)
            {
                if(grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
}
