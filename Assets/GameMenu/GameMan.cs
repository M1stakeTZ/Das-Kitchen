using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMan : MonoBehaviour
{
    public Text max;
    public void changeName1(string newName) {Stat.namePlayers[0] = newName;}
    public void changeName2(string newName) {Stat.namePlayers[1] = newName;}
    public void changeName3(string newName) {Stat.namePlayers[2] = newName;}
    public void changeName4(string newName) {Stat.namePlayers[3] = newName;}

    public void changePlayerCountPlus() { Stat.playerCount++; }
    public void changePlayerCountMinus() { Stat.playerCount--; }

    public void back()
    {
        SceneManager.LoadScene(0);  
    }
    public void play()
    {
        SceneManager.LoadScene(2);
    }

    public void setPoints()
    {
        Invoke("sp", 0.1f);
    }

    void sp()
    {
        int.TryParse(max.text, out int res);
        Stat.maxPoints = res;
    }
}
