using UnityEngine;

public class LightBulb : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer baseSprite;
    public SpriteRenderer colorSprite;
    public SpriteRenderer brokenSprite;

    [Header("Connection")]
    public bool isConnected = false;

    [Header("Resistance Settings")]
    public float targetResistance = 1000f;    // the resistance needed to light the bulb
    public float tolerance = 50f;             // how close the value needs to be (±)

    [Header("Current Resistance")]
    public float currentResistance = 0f;      // hook this up to your Resistor script

    void OnValidate()
    {
        UpdateBulb();
    }

    void Start()
    {
        UpdateBulb();
    }

    void Update()
    {
        UpdateBulb();
    }

    public void UpdateBulb()
    {
        if (!isConnected)
        {
            // Wire not connected — show only base
            SetSprites(showBase: true, showColor: false, showBroken: false);
        }
        else if (IsResistanceCorrect())
        {
            // Connected and correct resistance — bulb lights up
            SetSprites(showBase: true, showColor: true, showBroken: false);
        }
        else
        {
            // Connected but wrong resistance — blown bulb
            SetSprites(showBase: false, showColor: false, showBroken: true);
        }
    }

    private bool IsResistanceCorrect()
    {
        return Mathf.Abs(currentResistance - targetResistance) <= tolerance;
    }

    private void SetSprites(bool showBase, bool showColor, bool showBroken)
    {
        if (baseSprite   != null) baseSprite.enabled   = showBase;
        if (colorSprite  != null) colorSprite.enabled  = showColor;
        if (brokenSprite != null) brokenSprite.enabled = showBroken;
    }

    // Call this from other scripts to connect/disconnect the wire
    public void SetConnected(bool connected)
    {
        isConnected = connected;
        UpdateBulb();
    }

    // Call this from other scripts to pass in the resistor's value
    public void SetResistance(float resistance)
    {
        currentResistance = resistance;
        UpdateBulb();
    }
}