using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public Text winner;
    void Start()
    {
        winner.text = "онаедхрекэ: " + Stat.namePlayers[Stat.winner];
    }

    public void toLobby()
    {
        SceneManager.LoadScene(0);
    }
}
