using UnityEngine;

public class Roll : MonoBehaviour
{
    int count = 0;
    Rigidbody rb;
    public float torque = 1f;
    public float inv = 4f;
    public GameManager gm;
    public GameObject next;
    public GameObject last;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void roll()
    {
        if (gm.isBookOpen) return;
        gm.playSound();
        next.SetActive(true);
        last.SetActive(false);
        next.GetComponent<AudioSource>().Play();
        rb.isKinematic = false;
        gm.gamePhase = 2;
        rb.AddTorque(new Vector3(100 * torque, 0f, 0f));
        Invoke("getInt", inv);
    }

    void getInt()
    {
        rb.isKinematic = true;
        count = Random.Range(1, 7);
        Stat.roll = count;

        if (count == 1) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else if (count == 2) transform.eulerAngles = new Vector3 (0f, 270f, 0f);
        else if (count == 3) transform.eulerAngles = new Vector3(90f, 180f, 0f);
        else if (count == 4) transform.eulerAngles = new Vector3(90f, 0f, 0f);
        else if (count == 5) transform.eulerAngles = new Vector3(0f, 90f, 0f);
        else transform.eulerAngles = new Vector3(0f, 180f, 0f);
        gm.hod();
    }
}
