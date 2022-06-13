using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public GameManager gm;
    bool canRotate = false;
    bool canDestroy = false;
    Sprite spriteFront;
    public Sprite spriteBack;
    int znak = 1;

    private void FixedUpdate()
    {
        if (canRotate)
        {
            if (transform.rotation.y >= .6691306f && transform.rotation.y <= .7f) { GetComponent<Image>().sprite = spriteFront; znak = -1; }
            if (transform.rotation.y < 0.01f && znak == -1) { canRotate = false; gm.animEnd(); znak = 1; gm.gamePhase = 0; }
            else transform.Rotate(0, 3 * znak, 0);
        }

        if (canDestroy)
        {
            Vector2 size = GetComponent<RectTransform>().sizeDelta;
            GetComponent<RectTransform>().sizeDelta = new Vector2((int)(size.x - 4.3f), (int)(size.y - 6.2f));

            if (size.x <= 30f)
            {
                transform.Rotate(0f, -transform.rotation.y, 0f);
                canDestroy = false;
                gameObject.SetActive(false);
                GetComponent<RectTransform>().sizeDelta = new Vector2(215f, 310f);
                GetComponent<Image>().sprite = spriteBack;
                gm.canEndHod();
            }
        }
    }

    public void changeImage(Sprite spr)
    {
        spriteFront = spr;
        canRotate = true;
    }

    public void sendToGM()
    {
        gm.setSprite(transform.position);
    }

    public void destroy()
    {
        canDestroy = true;
    }
}
