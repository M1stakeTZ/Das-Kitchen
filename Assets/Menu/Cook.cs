using UnityEngine;

public class Cook : MonoBehaviour
{
    Animator anim;
    int count = 0;
    public int max = 300;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (count == 73)
        {
            anim.SetInteger("arms", 5);
        }
        if (count == max)
        {
            anim.SetInteger("arms", 16);
            count = 0;
        }
        count++;
    }
    public void quitAnim()
    {
        count = max + 1;
        anim.SetInteger("arms", 6);
        anim.SetInteger("legs", 5);
    }
    public void settingsAnim()
    {
        count = max + 1;
        anim.SetInteger("arms", 9);
        anim.SetInteger("legs", 5);
    }
    public void playAnim()
    {
        count = max + 1;
        anim.SetInteger("arms", 2);
        anim.SetInteger("legs", 2);
    }
    public void returnAnim()
    {
        count = (max - 73) / 2;
        anim.SetInteger("arms", 5);
        anim.SetInteger("legs", 1);
    }
}
