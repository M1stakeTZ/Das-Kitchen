using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int[][] map = new int[60][];
    public Sprite[] ing;
    public Sprite[] kletki;
    public Sprite[] butons;
    public GameObject basic;
    public Text namePlayer;
    public Text points;
    public GameObject parent;
    public GameObject arrow;

    public GameObject basicIng;
    public GameObject frontParent;
    public AudioSource butonPress;

    public GameObject[] players;
    public GameObject[] rolled;
    public GameObject[] icons;

    int[] localKletka = new int[4] { 1, 1, 1, 1 };
    int[][] playerIng = new int[4][];
    public Player[] playerClass;
    GameObject[] kletka = new GameObject[60];
    public GameObject[] cards;
    public GameObject[] cardsActive;
    public GameObject strela;
    public GameObject tp;
    public GameObject click;

    public GameObject book;
    public GameObject[] ingerdients;
    public GameObject bookButon;
    public Sprite[] butonSprite;
    public Sprite[] reciepSprites;
    public Sprite[] arrowSprites;
    public GameObject[] arrows;
    public GameObject actived;
    public GameObject price;
    int openRec = 0;
    bool canDo = false;

    public GameObject rand;
    int roddom = 0;
    public AudioSource topMusic;

    int localPlayer = 3;
    int[][] ways = new int[16][];
    int indexWays = 0;
    int[] randForInts;
    bool isInvSet = false;
    public int gamePhase = 0;
    public bool isBookOpen = false;

    int[,] rec = new int[11, 5];
    int[] bookRec = new int[6] { -1, -1, -1, -1, -1, -1 };
    void Start()
    {
        setMap();
        if (Stat.maxPoints <= 0) Stat.maxPoints = 1;

        for (int i = 0; i < map.Length; i++)
        {
            GameObject arr = null;
            if (map[i][0] != 0)
            {
                arr = Instantiate(arrow, new Vector3(map[i][5] * 120 + 500, map[i][6] * 125 + 156, 0), new Quaternion(0, 0, 0, 0));
                arr.transform.SetParent(parent.transform);
                arr.transform.Rotate(0, 0, 90);
            }
            if (map[i][1] != 0)
            {
                arr = Instantiate(arrow, new Vector3(map[i][5] * 120 + 560, map[i][6] * 125 + 94, 0), new Quaternion(0, 0, 0, 0));
                arr.transform.SetParent(parent.transform);
            }
            if (map[i][2] != 0)
            {
                arr = Instantiate(arrow, new Vector3(map[i][5] * 120 + 500, map[i][6] * 125 + 32, 0), new Quaternion(0, 0, 270, 0));
                arr.transform.SetParent(parent.transform);
                arr.transform.Rotate(0, 0, 90);
            }
            if (map[i][3] != 0)
            {
                arr = Instantiate(arrow, new Vector3(map[i][5] * 120 + 440, map[i][6] * 125 + 94, 0), new Quaternion(0, 0, 180, 0));
                arr.transform.SetParent(parent.transform);
            }
        }
        for (int i = 0; i < map.Length; i++)
        {
            GameObject go = Instantiate(basic, new Vector3(map[i][5] * 120 + 500, map[i][6] * 125 + 94, 0), new Quaternion(0, 0, 0, 0));
            go.transform.SetParent(parent.transform);
            go.GetComponent<Image>().sprite = kletki[map[i][4]];

            kletka[i] = go;
        }

        if (Stat.playerCount > 2) players[2].SetActive(true);
        if (Stat.playerCount > 3) players[3].SetActive(true);

        for (int i = 0; i < 4; i++) { playerIng[i] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; }

        genRec();

        rec = new int[,] {
            { 10, 13, 17, 0, 100 },
            { 8, 2, 4, 13, 150 },
            { 12, 8, 15, 0, 100 },
            { 4, 17, 18, 14, 200 },
            { 14, 15, 2, 0, 150 },
            { 1, 3, 5, 6, 200 },
            { 1, 5, 8, 4, 150 },
            { 11, 15, 8, 0, 150 },
            { 16, 18, 17, 14, 200 },
            { 1, 9, 4, 7, 150 },
            { 2, 5, 6, 0, 150 },
        };

        next();
    }

    public void hodEnd()
    {
        gamePhase = 2;
        int select = map[localKletka[localPlayer] - 1][4];
        if (select <= 1)
        {
            canEndHod();
        }
        else if (select == 5 || select == 6 || select == 4)
        {
            if (playerIng[localPlayer][8] != 0) 
            {
                foreach (GameObject card in cardsActive) { card.SetActive(true); } strela.SetActive(true);
            }
            else foreach (GameObject card in cards)
                {
                    card.SetActive(true);
                }
        }
        else if (select == 3)
        {
            for (int i = 0; i < 60; i++)
            {
                if (map[i][4] == 3) if (i != localKletka[localPlayer] - 1)
                    {
                        kletka[i].GetComponent<SetSelected>().clickPrefab = tp;
                        kletka[i].GetComponent<SetSelected>().Select();
                        kletka[i].GetComponent<SetSelected>().clickPrefab = click;
                        Instantiate(tp, new Vector3(map[i][5] * 120 + 500, map[i][6] * 125 + 94, 0), new Quaternion(0, 0, 0, 0));
                    }
            }
        }
        else if (select == 2)
        {
            roddom = Random.Range(0, 5);
            rand.GetComponent<Rand>().Go(Stat.rands[roddom]);
        }
    }

    public void next()
    {
        if (isBookOpen) closeBook();
        for (int i = 0; i < 16; i++)
        {
            ways[i] = new int[] { 0, 0, 0, 0, 0, 0 };
        }
        indexWays = 0;
        icons[localPlayer].SetActive(false);
        rolled[0].SetActive(true);
        rolled[1].SetActive(false);
        rolled[1].GetComponent<Button>().image.sprite = butons[0];
        rolled[1].GetComponent<Button>().interactable = false;
        localPlayer++;
        if (localPlayer > Stat.playerCount - 1) localPlayer = 0;
        icons[localPlayer].SetActive(true);
        namePlayer.text = Stat.namePlayers[localPlayer];
        points.text = $"ДЕНЬГИ: {Stat.pointsPlayers[localPlayer]}/{Stat.maxPoints}";
        isInvSet = false;
        setInventory();
    }

    public void hod()
    {
        int rolled = Stat.roll;

        recursiveFun(rolled, localKletka[localPlayer], 0);
    }

    void recursiveFun(int roll, int kl, int way)
    {
        kl--;
        if (Stat.roll != roll)
        {
            ways[way][Stat.roll - roll - 1] = kl;
        }
        for (int i = Stat.roll - roll; i < Stat.roll; i++)
        {
            roll--;
            if ((map[kl][0] != 0 && map[kl][1] == 0 && map[kl][2] == 0 && map[kl][3] == 0) || (map[kl][0] == 0 && map[kl][1] != 0 &&
                map[kl][2] == 0 && map[kl][3] == 0) || (map[kl][0] == 0 && map[kl][1] == 0 && map[kl][2] != 0 && map[kl][3] == 0) ||
                (map[kl][0] == 0 && map[kl][1] == 0 && map[kl][2] == 0 && map[kl][3] != 0))
            {
                kl = Mathf.Max(new int[4] { map[kl][0], map[kl][1], map[kl][2], map[kl][3] }) - 1;
            }
            else
            {
                int[] arr = new int[4] { map[kl][0], map[kl][1], map[kl][2], map[kl][3] };
                kl = Mathf.Max(arr);
                indexWays++;
                if (arr[0] == kl)
                {
                    recursiveFun(roll, Mathf.Max(new int[3] { arr[1], arr[2], arr[3] }), indexWays);
                }
                if (arr[1] == kl)
                {
                    recursiveFun(roll, Mathf.Max(new int[3] { arr[0], arr[2], arr[3] }), indexWays);
                }
                if (arr[2] == kl)
                {
                    recursiveFun(roll, Mathf.Max(new int[3] { arr[1], arr[0], arr[3] }), indexWays);
                }
                if (arr[3] == kl)
                {
                    recursiveFun(roll, Mathf.Max(new int[3] { arr[1], arr[2], arr[0] }), indexWays);
                }
                for (int j = 0; j < i; j++)
                {
                    ways[indexWays][j] = ways[way][j];
                }
                kl--;
            }
            ways[way][i] = kl;
        }
        kletka[kl].GetComponent<SetSelected>().Select();
    }

    public void moving(Vector3 v3)
    {
        foreach (GameObject goes in GameObject.FindGameObjectsWithTag("Selected")) { Destroy(goes); }
        foreach (GameObject goes in GameObject.FindGameObjectsWithTag("Click")) { Destroy(goes); }
        Vector3[] v = new Vector3[Stat.roll];
        for (int i = 0; i < 16; i++)
        {
            if (map[ways[i][Stat.roll - 1]][5] * 120 + 500 == (int)v3.x && map[ways[i][Stat.roll - 1]][6] * 125 + 94 == (int)v3.y)
            {
                for (int j = 0; j < Stat.roll; j++)
                {
                    v[j] = new Vector3(map[ways[i][j]][5] * 120 + 500, map[ways[i][j]][6] * 125 + 114, 0);
                }

                playerClass[localPlayer].GetComponent<Player>().moving(v);
                localKletka[localPlayer] = ways[i][Stat.roll - 1] + 1;
                break;
            }
        }
    }

    void setMap()
    {
        map[0] = new int[] { 2, 0, 0, 0, 0, 0, 0 };
        map[1] = new int[] { 3, 0, 0, 0, 0, 0, 1 };
        map[2] = new int[] { 0, 5, 0, 0, 0, 0, 2 };
        map[3] = new int[] { 0, 0, 0, 1, 0, 1, 0 };
        map[4] = new int[] { 6, 12, 0, 0, 0, 1, 2 };
        map[5] = new int[] { 7, 0, 0, 0, 0, 1, 3 };
        map[6] = new int[] { 8, 0, 0, 0, 0, 1, 4 };
        map[7] = new int[] { 9, 0, 0, 0, 0, 1, 5 };
        map[8] = new int[] { 10, 0, 0, 0, 0, 1, 6 };
        map[9] = new int[] { 0, 13, 0, 0, 0, 1, 7 };
        map[10] = new int[] { 0, 0, 0, 4, 0, 2, 0 };
        map[11] = new int[] { 0, 16, 0, 0, 0, 2, 2 };
        map[12] = new int[] { 0, 21, 0, 0, 0, 2, 7 };
        map[13] = new int[] { 0, 22, 0, 11, 0, 3, 0 };
        map[14] = new int[] { 0, 0, 14, 0, 0, 3, 1 };
        map[15] = new int[] { 17, 0, 15, 0, 0, 3, 2 };
        map[16] = new int[] { 18, 0, 0, 0, 0, 3, 3 };
        map[17] = new int[] { 19, 24, 0, 0, 0, 3, 4 };
        map[18] = new int[] { 20, 0, 0, 0, 0, 3, 5 };
        map[19] = new int[] { 21, 0, 0, 0, 0, 3, 6 };
        map[20] = new int[] { 0, 25, 0, 0, 0, 3, 7 };
        map[21] = new int[] { 0, 26, 0, 0, 0, 4, 0 };
        map[22] = new int[] { 0, 0, 0, 16, 0, 4, 2 };
        map[23] = new int[] { 0, 29, 0, 0, 0, 4, 4 };
        map[24] = new int[] { 0, 30, 0, 0, 0, 4, 7 };
        map[25] = new int[] { 0, 31, 0, 0, 0, 5, 0 };
        map[26] = new int[] { 0, 0, 0, 23, 0, 5, 2 };
        map[27] = new int[] { 0, 0, 27, 0, 0, 5, 3 };
        map[28] = new int[] { 0, 33, 28, 0, 0, 5, 4 };
        map[29] = new int[] { 0, 35, 0, 0, 0, 5, 7 };
        map[30] = new int[] { 0, 36, 0, 0, 0, 6, 0 };
        map[31] = new int[] { 0, 0, 0, 27, 0, 6, 2 };
        map[32] = new int[] { 34, 0, 0, 0, 0, 6, 4 };
        map[33] = new int[] { 0, 38, 0, 0, 0, 6, 5 };
        map[34] = new int[] { 0, 39, 0, 0, 0, 6, 7 };
        map[35] = new int[] { 0, 40, 0, 0, 0, 7, 0 };
        map[36] = new int[] { 0, 0, 0, 32, 0, 7, 2 };
        map[37] = new int[] { 0, 42, 0, 0, 0, 7, 5 };
        map[38] = new int[] { 0, 43, 0, 0, 0, 7, 7 };
        map[39] = new int[] { 0, 44, 0, 0, 0, 8, 0 };
        map[40] = new int[] { 0, 0, 0, 37, 0, 8, 2 };
        map[41] = new int[] { 0, 49, 0, 0, 0, 8, 5 };
        map[42] = new int[] { 0, 51, 0, 0, 0, 8, 7 };
        map[43] = new int[] { 45, 0, 0, 0, 0, 9, 0 };
        map[44] = new int[] { 46, 0, 0, 0, 0, 9, 1 };
        map[45] = new int[] { 0, 0, 0, 41, 0, 9, 2 };
        map[46] = new int[] { 0, 0, 46, 0, 0, 9, 3 };
        map[47] = new int[] { 0, 0, 47, 0, 0, 9, 4 };
        map[48] = new int[] { 50, 0, 48, 0, 0, 9, 5 };
        map[49] = new int[] { 51, 0, 0, 0, 0, 9, 6 };
        map[50] = new int[] { 0, 54, 0, 0, 0, 9, 7 };
        map[51] = new int[] { 0, 0, 0, 46, 0, 10, 2 };
        map[52] = new int[] { 0, 0, 0, 49, 0, 10, 5 };
        map[53] = new int[] { 0, 60, 0, 0, 0, 10, 7 };
        map[54] = new int[] { 0, 0, 0, 52, 0, 11, 2 };
        map[55] = new int[] { 0, 0, 55, 0, 0, 11, 3 };
        map[56] = new int[] { 0, 0, 56, 0, 0, 11, 4 };
        map[57] = new int[] { 0, 0, 57, 53, 0, 11, 5 };
        map[58] = new int[] { 0, 0, 58, 0, 0, 11, 6 };
        map[59] = new int[] { 0, 0, 59, 0, 0, 11, 7 };

        //gen(2, 59); return;
        gen(1, 8);  //Кухни
        gen(2, 5);  //?
        gen(3, 5);  //Телепорт
        gen(4, 9); //Ингредиент 1
        gen(5, 9); //Ингредиент 2
        gen(6, 9); //Ингредиент 3
    }

    void gen(int num, int c)
    {
        for (int i = 0; i < c; i++)
        {
            //break;
            while (true)
            {
                int r = Random.Range(1, 60);
                if (map[r][4] == 0)
                {
                    map[r][4] = num;
                    break;
                }
            }
        }
    }

    public void setSprite(Vector3 pos)
    {
        int randIng = 0;

        if (map[localKletka[localPlayer] - 1][4] == 4) randForInts = new int[7] { 1, 2, 3, 4, 5, 6, 19 };
        if (map[localKletka[localPlayer] - 1][4] == 5) randForInts = new int[7] { 7, 8, 9, 10, 11, 12, 19 };
        if (map[localKletka[localPlayer] - 1][4] == 6) randForInts = new int[7] { 13, 14, 15, 16, 17, 18, 19 };

        for (int i = 0; i < 7; i++)
        {
            randIng = Random.Range(0, 7 - i);

            cards[i].GetComponent<Rotate>().changeImage(ing[randForInts[randIng]]);

            if (cards[i].transform.position == pos)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (playerIng[localPlayer][j] == 0)
                    {
                        playerIng[localPlayer][j] = randForInts[randIng];
                        break;
                    }
                }
            }
            randForInts = randForInts.RemoveAt(randIng);
        }
    }

    public void animEnd()
    {
        if (!isInvSet)
        {
            isInvSet = true;
            setInventory();
            foreach (GameObject card in cards) { card.GetComponent<Rotate>().destroy(); }
        }
    }

    public void canEndHod()
    {
        gamePhase = 0;
        rolled[1].GetComponent<Button>().image.sprite = butons[1];
        rolled[1].GetComponent<Button>().interactable = true;
    }

    void setInventory()
    {
        int cycle = 0;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(basicIng, new Vector3(-i * 115 + 295, 465 + j * 160, 0), new Quaternion(0, 0, 0, 0));
                go.transform.SetParent(frontParent.transform);
                go.GetComponent<Image>().sprite = ing[playerIng[localPlayer][8 - cycle]];
                cycle++;
            }
        }
    }

    public void delEl(Vector3 pos)
    {
        int index = (2 - ((int)pos.y - 465) / 160) * 3 + (((int)pos.x - 65) / 115);
        playerIng[localPlayer][index] = 0;

        for (int i = 0; i < 9; i++)
        {
            if (i > index)
            {
                playerIng[localPlayer][i - 1] = playerIng[localPlayer][i];
            }
        }
        playerIng[localPlayer][8] = 0;
        strela.SetActive(false);
        setInventory();

        foreach (GameObject card in cardsActive)
        {
            card.SetActive(false);
        }
        foreach (GameObject card in cards)
        {
            card.SetActive(true);
        }
    }

    public void teleport(Vector3 pos)
    {
        playerClass[localPlayer].tpTo(pos);

        for (int i = 0; i < 60; i++)
        {
            if (map[i][5] * 120 + 500 == (int)pos.x && map[i][6] * 125 + 94 == (int)pos.y)
            {
                localKletka[localPlayer] = i + 1;
                break;
            }
        }

        foreach (GameObject telep in GameObject.FindGameObjectsWithTag("tp")) { Destroy(telep); }
        foreach (GameObject telep in GameObject.FindGameObjectsWithTag("Selected")) { Destroy(telep); }
    }

    public void openBook()
    {
        if (gamePhase >= 2) return;
        if (!isBookOpen)
        {
            arrows[0].GetComponent<Image>().sprite = arrowSprites[0];
            arrows[1].GetComponent<Image>().sprite = arrowSprites[1];
            actived.SetActive(false);
        }

        book.SetActive(true);
        isBookOpen = true;
        book.GetComponent<Image>().sprite = reciepSprites[bookRec[openRec]];
        price.GetComponent<Text>().text = rec[bookRec[openRec], 4].ToString();

        int canCook = 0;

        for (int i = 0; i < 4; i++)
        {
            ingerdients[i].GetComponent<Text>().color = Color.red;
            if (rec[bookRec[openRec], i] == 0) ingerdients[i].GetComponent<Text>().text = "";
            else ingerdients[i].GetComponent<Text>().text = Stat.ingerd[rec[bookRec[openRec], i] - 1];
        }

        for (int i = 0; i < 4; i++)
        {
            if (rec[bookRec[openRec], i] == 0)
            {
                canCook += 4 - i;
                break;
            }

            for (int j = 0; j < 9; j++)
            {
                if (playerIng[localPlayer][j] == rec[bookRec[openRec], i])
                {
                    canCook++;
                    ingerdients[i].GetComponent<Text>().color = Color.green;
                    break;
                }
            }
        }

        if (canCook == 4 && map[localKletka[localPlayer] - 1][4] == 1 && openRec < 4)
        { 
            bookButon.GetComponent<Image>().sprite = butonSprite[1]; 
            canDo = true; 
        }
        else bookButon.GetComponent<Image>().sprite = butonSprite[0];
    }

    public void closeBook()
    {
        book.SetActive(false);
        canDo = false;
        isBookOpen = false;
        openRec = 0;
    }

    public void playSound()
    {
        butonPress.Play();
    }

    public void moveBook(int isFront)
    {
        playSound();
        if (openRec == 5 && isFront == 1) return;
        if (openRec == 0 && isFront == -1) return;
        canDo = false;
        openRec += isFront;

        switch (openRec)
        {
            case 0:
                arrows[0].GetComponent<Image>().sprite = arrowSprites[0];
                break;
            case 1:
                arrows[0].GetComponent<Image>().sprite = arrowSprites[1];
                break;
            case 3:
                actived.SetActive(false);
                break;
            case 4:
                arrows[1].GetComponent<Image>().sprite = arrowSprites[1];
                actived.SetActive(true);
                break;
            case 5:
                arrows[1].GetComponent<Image>().sprite = arrowSprites[0];
                break;
        }
        openBook();
    }

    void addRec(int index)
    {
        for (int i = index; i < 5; i++)
        {
            bookRec[i] = bookRec[i + 1];
        }

        while (true)
        {
            bookRec[5] = Random.Range(0, 11);
            if (bookRec[0] != bookRec[5] && bookRec[1] != bookRec[5] && bookRec[2] != bookRec[5] && bookRec[3] != bookRec[5] && bookRec[4] != bookRec[5]) break;
        }
    }

    void genRec()
    {
        for (int i = 0; i < 6; i++)
        {
            while (true)
            {
                bookRec[i] = Random.Range(0, 11);
                bool canEnd = true;
                for (int j = 0; j < i; j++)
                {
                    if (bookRec[i] == bookRec[j]) canEnd = false;
                }
                if (canEnd) break;
            }

        }
    }

    public void cook()
    {
        playSound();
        if (map[localKletka[localPlayer] - 1][4] == 1)
        {
            if (canDo && openRec < 4)
            {
                Stat.pointsPlayers[localPlayer] += rec[openRec, 4];
                points.text = $"ДЕНЬГИ: {Stat.pointsPlayers[localPlayer]}/{Stat.maxPoints}";
                if (Stat.pointsPlayers[localPlayer] >= Stat.maxPoints)
                {
                    Stat.winner = localPlayer;
                    SceneManager.LoadScene(3);
                }
                int max = 4;
                if (rec[bookRec[openRec], 3] == 0) max--;
                if (rec[bookRec[openRec], 2] == 0) max--;
                for (int i = 0; i < max; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (rec[bookRec[openRec], i] == playerIng[localPlayer][j])
                        {
                            playerIng[localPlayer][j] = 0;
                            break;
                        }
                    }
                }
                sortArr();
                setInventory();
                addRec(openRec);
                closeBook();
            }
        }
    }

    void sortArr()
    {
        int i = 0;
        int j = 0;
        for (; j < 9; j++) {
            if (playerIng[localPlayer][j] != 0)
            {
                if (i < j)
                {
                    int prom = playerIng[localPlayer][i];
                    playerIng[localPlayer][i] = playerIng[localPlayer][j];
                    playerIng[localPlayer][j] = prom;
                }
                i++;
            }
        }
    }

    public void randEnd()
    {
        switch (roddom)
        {
            case 0:
                Stat.pointsPlayers[localPlayer] += 50;
                points.text = $"ДЕНЬГИ: {Stat.pointsPlayers[localPlayer]}/{Stat.maxPoints}";
                if (Stat.pointsPlayers[localPlayer] >= Stat.maxPoints)
                {
                    Stat.winner = localPlayer;
                    SceneManager.LoadScene(3);
                }
                canEndHod();
                break;
            case 1:
                localKletka[localPlayer] = 1;
                playerClass[localPlayer].tpTo(new Vector3(500, 94, 0));
                break;
            case 2:
                for (int i = 0; i < 9; i++)
                {
                    int inger = playerIng[localPlayer][i];
                    if (inger == 5 || inger == 6 || inger >= 13) { playerIng[localPlayer][i] = 19; setInventory(); }
                }
                canEndHod();
                break;
            case 3:
                int whichPl = 0;
                while (true)
                {
                    whichPl = Random.Range(0, Stat.playerCount);
                    if (whichPl != localPlayer) break;
                }
                int kletka = localKletka[whichPl];
                localKletka[whichPl] = localKletka[localPlayer];
                localKletka[localPlayer] = kletka;
                playerClass[localPlayer].tpTo(new Vector3(
                    map[localKletka[localPlayer] - 1][5] * 120 + 500, map[localKletka[localPlayer] - 1][6] * 125 + 94, 0));
                playerClass[whichPl].tpTo(new Vector3(
                    map[localKletka[whichPl] - 1][5] * 120 + 500, map[localKletka[whichPl] - 1][6] * 125 + 94, 0));
                break;
            case 4:
                topMusic.Pause();
                rand.GetComponent<AudioSource>().Play();
                Invoke("resume", 5f);
                break;

        }
    }

    void resume()
    {
        topMusic.Play();
        canEndHod();
    }
}

