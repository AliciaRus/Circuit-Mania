using UnityEngine;
using UnityEngine.UI;

public class ConnectButton : MonoBehaviour
{
    public LightBulb lightBulb;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (lightBulb != null)
            lightBulb.SetConnected(true);
    }
}