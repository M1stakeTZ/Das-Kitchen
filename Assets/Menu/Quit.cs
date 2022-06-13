using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    public void quit()
    {
        Application.Quit();
    }
    public void play()
    {
        SceneManager.LoadScene(1);
    }
}
