using UnityEngine;
using UnityEngine.UI;

public class clickEvent : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(() => gm.moving(transform.position));
    }
}
