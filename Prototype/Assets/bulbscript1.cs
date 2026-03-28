using UnityEngine;

public class bulbscript1 : MonoBehaviour
{
    public bool bulb1 = false;
    public SpriteRenderer bulb_broken;
    public SpriteRenderer base_color;
    public SpriteRenderer bulb_base;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        if (bulb1 == false)
        {
            bulb_broken.color = new Color(1, 1, 1, 0); // opacity 0 (invisible)
        }
        else
        {
            bulb_broken.color = new Color(1, 1, 1, 1); // opacity 1 (visible)
            base_color.color = new Color(1, 1, 1, 0); // opacity 0 (invisible)
            bulb_base.color = new Color(1, 1, 1, 0); // opacity 0 (invisible)
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            bulb1 = true;
        }
    }
}
