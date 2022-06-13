using UnityEngine;

public class Anim : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

        returnAnim();
    }

    // Update is called once per frame
    public void returnAnim()
    {
        anim.SetInteger("legs", 1);
        anim.SetInteger("arms", 1);
    }

    public void play()
    {
        anim.SetInteger("legs", 5);
        anim.SetInteger("arms", 17);
    }
    public void back()
    {
        anim.SetInteger("legs", 36);
        anim.SetInteger("arms", 5);
    }
}
