using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Vector3 vector = new Vector3(500, 114, 0);
    public float speed = 1f;
    public float waitSec = 0.1f;
    public GameManager gm;
    public AudioSource tp;

    void FixedUpdate()
    {
        if ((vector - transform.position).magnitude > 4) transform.Translate((vector - transform.position).normalized * speed);
    }

    IEnumerator setTo(Vector3 v3, int long_)
    {
        yield return new WaitForSeconds(waitSec*long_);
        vector = v3;
    }
    public void moving(Vector3[] v3)
    {
        for (int i = 0; i < v3.Length; i++)
        {
            StartCoroutine(setTo(v3[i], i));
        }
        StartCoroutine(hodEnd(v3.Length));
    }

    IEnumerator hodEnd(int wait)
    {
        yield return new WaitForSeconds(waitSec * wait);
        gm.hodEnd();
    }

    public void tpTo(Vector3 pos)
    {
        vector = pos + Vector3.up * 20;
        transform.Translate(pos - transform.position);
        gm.canEndHod();
        tp.Play();
    }
}