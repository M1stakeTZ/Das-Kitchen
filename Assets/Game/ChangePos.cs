using UnityEngine;

public class ChangePos : MonoBehaviour
{
    void change(int x)
    {
        transform.position = new Vector3(x + 960, 668, 0);
    }

    public void change1() { change(-742); }
    public void change2() { change(-280); }
    public void change3() { change(182); }
}
