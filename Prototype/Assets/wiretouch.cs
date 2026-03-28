using UnityEngine;

public class wiretouch : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.name = "wiretouch";
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "wire")
        {
            myRigidbody2D.bodyType = RigidbodyType2D.Static;
        }
    }
}
