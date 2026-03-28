using UnityEngine;
using UnityEngine.UI;

public class ResistorPopup : MonoBehaviour
{
    private Resistor resistor;
    private GameObject popupPanel;
    private Dropdown band1Dropdown;
    private Dropdown band2Dropdown;
    private Dropdown band3Dropdown;
    private Dropdown band4Dropdown;

    private readonly string[] colorOptions = new string[]
    {
        "Black", "Brown", "Red", "Orange", "Yellow",
        "Green", "Blue", "Violet", "Grey", "White", "Gold", "Silver"
    };

    void Start()
    {
        resistor = GetComponent<Resistor>();

        // Delete any existing PopupPanel first
        GameObject existing = GameObject.Find("PopupPanel");
        if (existing != null) Destroy(existing);

        CreatePopupUI();
        popupPanel.SetActive(false);
    }

    void CreatePopupUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        popupPanel = new GameObject("PopupPanel");
        popupPanel.transform.SetParent(canvas.transform, false);
        RectTransform panelRect = popupPanel.AddComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(400, 280);
        panelRect.anchoredPosition = Vector2.zero;
        Image panelImage = popupPanel.AddComponent<Image>();
        panelImage.color = new Color(0.15f, 0.15f, 0.15f, 0.95f);

        band1Dropdown = CreateDropdown("Band 1", 100);
        band2Dropdown = CreateDropdown("Band 2", 40);
        band3Dropdown = CreateDropdown("Band 3", -20);
        band4Dropdown = CreateDropdown("Band 4", -80);

        // Close button
        GameObject btnObj = new GameObject("CloseButton");
        btnObj.transform.SetParent(popupPanel.transform, false);
        RectTransform btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.sizeDelta = new Vector2(120, 35);
        btnRect.anchoredPosition = new Vector2(0, -130);
        Image btnImage = btnObj.AddComponent<Image>();
        btnImage.color = new Color(0.8f, 0.2f, 0.2f);
        Button btn = btnObj.AddComponent<Button>();
        btn.onClick.AddListener(ClosePopup);

        // Button text
        GameObject btnText = new GameObject("Text");
        btnText.transform.SetParent(btnObj.transform, false);
        Text txt = btnText.AddComponent<Text>();
        txt.text = "Close";
        txt.alignment = TextAnchor.MiddleCenter;
        txt.fontSize = 18;
        txt.color = Color.white;
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        RectTransform txtRect = btnText.GetComponent<RectTransform>();
        txtRect.sizeDelta = new Vector2(120, 35);
        txtRect.anchoredPosition = Vector2.zero;

        band1Dropdown.value = (int)resistor.band1;
        band2Dropdown.value = (int)resistor.band2;
        band3Dropdown.value = (int)resistor.band3;
        band4Dropdown.value = (int)resistor.band4;

        band1Dropdown.onValueChanged.AddListener((_) => ApplyColors());
        band2Dropdown.onValueChanged.AddListener((_) => ApplyColors());
        band3Dropdown.onValueChanged.AddListener((_) => ApplyColors());
        band4Dropdown.onValueChanged.AddListener((_) => ApplyColors());
    }

    Dropdown CreateDropdown(string label, float posY)
    {
        GameObject obj = new GameObject(label);
        obj.transform.SetParent(popupPanel.transform, false);
        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(350, 40);
        rect.anchoredPosition = new Vector2(0, posY);
        Image img = obj.AddComponent<Image>();
        img.color = Color.white;

        Dropdown dd = obj.AddComponent<Dropdown>();

        // Create dropdown text
        GameObject textObj = new GameObject("Label");
        textObj.transform.SetParent(obj.transform, false);
        Text textComp = textObj.AddComponent<Text>();
        textComp.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComp.color = Color.black;
        textComp.fontSize = 16;
        textComp.alignment = TextAnchor.MiddleLeft;
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.offsetMin = new Vector2(10, 0);
        textRect.offsetMax = new Vector2(-25, 0);
        dd.captionText = textComp;

        // Create template
        GameObject template = new GameObject("Template");
        template.transform.SetParent(obj.transform, false);
        RectTransform templateRect = template.AddComponent<RectTransform>();
        templateRect.anchorMin = new Vector2(0, 0);
        templateRect.anchorMax = new Vector2(1, 0);
        templateRect.pivot = new Vector2(0.5f, 1f);
        templateRect.sizeDelta = new Vector2(0, 150);
        templateRect.anchoredPosition = Vector2.zero;
        Image templateImg = template.AddComponent<Image>();
        templateImg.color = Color.white;
        ScrollRect scrollRect = template.AddComponent<ScrollRect>();

        GameObject viewport = new GameObject("Viewport");
        viewport.transform.SetParent(template.transform, false);
        RectTransform viewportRect = viewport.AddComponent<RectTransform>();
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.sizeDelta = Vector2.zero;
        viewportRect.pivot = new Vector2(0, 1);
        viewport.AddComponent<Image>();
        viewport.AddComponent<Mask>().showMaskGraphic = false;
        scrollRect.viewport = viewportRect;

        GameObject content = new GameObject("Content");
        content.transform.SetParent(viewport.transform, false);
        RectTransform contentRect = content.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.pivot = new Vector2(0.5f, 1f);
        contentRect.sizeDelta = new Vector2(0, 40);
        scrollRect.content = contentRect;

        GameObject item = new GameObject("Item");
        item.transform.SetParent(content.transform, false);
        RectTransform itemRect = item.AddComponent<RectTransform>();
        itemRect.anchorMin = new Vector2(0, 0.5f);
        itemRect.anchorMax = new Vector2(1, 0.5f);
        itemRect.sizeDelta = new Vector2(0, 40);
        Toggle toggle = item.AddComponent<Toggle>();

        GameObject itemBg = new GameObject("Item Background");
        itemBg.transform.SetParent(item.transform, false);
        RectTransform itemBgRect = itemBg.AddComponent<RectTransform>();
        itemBgRect.anchorMin = Vector2.zero;
        itemBgRect.anchorMax = Vector2.one;
        itemBgRect.sizeDelta = Vector2.zero;
        Image itemBgImg = itemBg.AddComponent<Image>();
        itemBgImg.color = new Color(0.9f, 0.9f, 0.9f);

        GameObject itemText = new GameObject("Item Label");
        itemText.transform.SetParent(item.transform, false);
        Text itemTextComp = itemText.AddComponent<Text>();
        itemTextComp.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        itemTextComp.color = Color.black;
        itemTextComp.fontSize = 16;
        itemTextComp.alignment = TextAnchor.MiddleLeft;
        RectTransform itemTextRect = itemText.GetComponent<RectTransform>();
        itemTextRect.anchorMin = Vector2.zero;
        itemTextRect.anchorMax = Vector2.one;
        itemTextRect.offsetMin = new Vector2(10, 0);
        itemTextRect.offsetMax = Vector2.zero;
        dd.itemText = itemTextComp;

        toggle.targetGraphic = itemBgImg;
        dd.template = templateRect;
        template.SetActive(false);

        dd.ClearOptions();
        dd.AddOptions(new System.Collections.Generic.List<string>(colorOptions));
        return dd;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!popupPanel.activeSelf)
                {
                    popupPanel.SetActive(true);
                }
            }
        }
    }

    void ApplyColors()
    {
        resistor.band1 = (ResistorColor)band1Dropdown.value;
        resistor.band2 = (ResistorColor)band2Dropdown.value;
        resistor.band3 = (ResistorColor)band3Dropdown.value;
        resistor.band4 = (ResistorColor)band4Dropdown.value;
        resistor.UpdateBandColors();
        resistor.CalculateResistance();
    }

    void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}