using UnityEngine;

public class tp : MonoBehaviour
{
    GameManager gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void sendToGM()
    {
        gm.teleport(transform.position);
    }
}
