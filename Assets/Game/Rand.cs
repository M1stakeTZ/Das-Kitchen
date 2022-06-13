using UnityEngine;
using UnityEngine.UI;

public class Rand : MonoBehaviour
{
    public GameObject text;
    public GameManager gm;
    bool canGo = false;

    void Update()
    {
        if (canGo)
        {
            if ((int)transform.position.y > 540) transform.Translate(0, -10, 0);
            else canGo = false;
        }
    }

    public void Go(string t)
    {
        canGo = true;
        text.GetComponent<Text>().text = t;
    }

    public void Stop()
    {
        transform.Translate(0, 1500, 0);
        gm.randEnd();
    }
}
