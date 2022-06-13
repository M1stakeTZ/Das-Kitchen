using UnityEngine;

public class SetSelected : MonoBehaviour
{
    public GameObject selectPrefab;
    public GameObject clickPrefab;
    GameObject parentSelelct;
    GameObject parentClick;

    private void Start()
    {
        parentSelelct = GameObject.FindGameObjectWithTag("BG");
        parentClick = GameObject.FindGameObjectWithTag("Frontend");
    }
    public void Select()
    {
        GameObject go1 = Instantiate(selectPrefab, transform.position, transform.rotation);
        go1.transform.SetParent(parentSelelct.transform);
        go1 = Instantiate(clickPrefab, transform.position, transform.rotation);
        go1.transform.SetParent(parentClick.transform);
    }
}
