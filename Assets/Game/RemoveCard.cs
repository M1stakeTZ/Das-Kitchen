using UnityEngine;

public class RemoveCard : MonoBehaviour
{

    public GameManager gm;
    public void sendToGM()
    {
        gm.delEl(transform.position);
    }
}
