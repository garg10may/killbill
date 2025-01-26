using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISetup : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private GameObject sliderPrefab;
    [SerializeField] private GameObject textPrefab;

    [Header("UI Scale")]
    [SerializeField] private Vector2 referenceResolution = new Vector2(1080, 1920); // Mobile portrait resolution
    [SerializeField] private float uiScale = 0.5f; // Adjust this to change overall UI scale

    [Header("Generated References")]
    public Slider healthBar;
    public Slider experienceBar;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI highScoreText;

    private UIManager uiManager;

    void Awake()
    {
        SetupCanvas();
        CreateTopPanel();
        SetupUIManager();
    }

    void SetupCanvas()
    {
        Canvas canvas = GetComponent<Canvas>();
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        
        if (canvas == null || scaler == null)
        {
            Debug.LogError("Canvas or CanvasScaler missing!");
            return;
        }

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = referenceResolution;
        scaler.matchWidthOrHeight = 0.5f;
    }

    void CreateTopPanel()
    {
        GameObject topPanel = CreatePanel("TopPanel");
        RectTransform topRect = topPanel.GetComponent<RectTransform>();
        
        topRect.anchorMin = new Vector2(0, 1);
        topRect.anchorMax = new Vector2(1, 1);
        topRect.pivot = new Vector2(0.5f, 1);
        topRect.sizeDelta = new Vector2(0, 100 * uiScale);
        topRect.anchoredPosition = Vector2.zero;

        float padding = 10 * uiScale;
        float elementWidth = (1080 - (padding * 6)) / 5; // Divide available space by 5 elements

        // Score (leftmost)
        scoreText = CreateText(topPanel, "ScoreText", "Score: 0");
        RectTransform scoreRect = scoreText.rectTransform;
        scoreRect.anchorMin = new Vector2(0, 0.5f);
        scoreRect.anchorMax = new Vector2(0, 0.5f);
        scoreRect.pivot = new Vector2(0, 0.5f);
        scoreRect.sizeDelta = new Vector2(elementWidth, 40 * uiScale);
        scoreRect.anchoredPosition = new Vector2(padding, 0);
        scoreText.fontSize = 24 * uiScale;

        // High Score
        highScoreText = CreateText(topPanel, "HighScoreText", "Best: 0");
        RectTransform highScoreRect = highScoreText.rectTransform;
        highScoreRect.anchorMin = new Vector2(0, 0.5f);
        highScoreRect.anchorMax = new Vector2(0, 0.5f);
        highScoreRect.pivot = new Vector2(0, 0.5f);
        highScoreRect.sizeDelta = new Vector2(elementWidth, 40 * uiScale);
        highScoreRect.anchoredPosition = new Vector2(padding * 2 + elementWidth, 0);
        highScoreText.fontSize = 24 * uiScale;

        // Health Bar
        healthBar = CreateSlider(topPanel, "HealthBar", Color.red);
        RectTransform healthRect = healthBar.GetComponent<RectTransform>();
        healthRect.anchorMin = new Vector2(0, 0.5f);
        healthRect.anchorMax = new Vector2(0, 0.5f);
        healthRect.pivot = new Vector2(0, 0.5f);
        healthRect.sizeDelta = new Vector2(elementWidth, 25 * uiScale);
        healthRect.anchoredPosition = new Vector2(padding * 3 + elementWidth * 2, 0);

        healthText = CreateText(healthBar.gameObject, "HealthText", "100/100");
        RectTransform healthTextRect = healthText.rectTransform;
        healthTextRect.anchorMin = new Vector2(0.5f, 0.5f);
        healthTextRect.anchorMax = new Vector2(0.5f, 0.5f);
        healthTextRect.pivot = new Vector2(0.5f, 0.5f);
        healthTextRect.sizeDelta = new Vector2(elementWidth * 0.8f, 25 * uiScale);
        healthTextRect.anchoredPosition = Vector2.zero;
        healthText.fontSize = 18 * uiScale;

        // Experience Bar
        experienceBar = CreateSlider(topPanel, "ExperienceBar", Color.blue);
        RectTransform expRect = experienceBar.GetComponent<RectTransform>();
        expRect.anchorMin = new Vector2(0, 0.5f);
        expRect.anchorMax = new Vector2(0, 0.5f);
        expRect.pivot = new Vector2(0, 0.5f);
        expRect.sizeDelta = new Vector2(elementWidth, 25 * uiScale);
        expRect.anchoredPosition = new Vector2(padding * 4 + elementWidth * 3, 0);

        // Level (rightmost)
        levelText = CreateText(topPanel, "LevelText", "Level 1");
        RectTransform levelRect = levelText.rectTransform;
        levelRect.anchorMin = new Vector2(0, 0.5f);
        levelRect.anchorMax = new Vector2(0, 0.5f);
        levelRect.pivot = new Vector2(0, 0.5f);
        levelRect.sizeDelta = new Vector2(elementWidth, 40 * uiScale);
        levelRect.anchoredPosition = new Vector2(padding * 5 + elementWidth * 4, 0);
        levelText.fontSize = 24 * uiScale;
    }

    void SetupUIManager()
    {
        // Add UIManager component if it doesn't exist
        uiManager = gameObject.AddComponent<UIManager>();
        
        // Assign references
        uiManager.healthBar = healthBar;
        uiManager.experienceBar = experienceBar;
        uiManager.scoreText = scoreText;
        uiManager.levelText = levelText;
        uiManager.healthText = healthText;
    }

    GameObject CreatePanel(string name)
    {
        GameObject panel = Instantiate(panelPrefab, transform);
        panel.name = name;
        Image image = panel.GetComponent<Image>();
        image.color = new Color(0, 0, 0, 0.5f);
        return panel;
    }

    Slider CreateSlider(GameObject parent, string name, Color fillColor)
    {
        GameObject sliderObj = Instantiate(sliderPrefab, parent.transform);
        sliderObj.name = name;
        Slider slider = sliderObj.GetComponent<Slider>();
        
        // Setup slider
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 1;
        
        // Set colors
        Image fill = slider.fillRect.GetComponent<Image>();
        fill.color = fillColor;
        Image background = slider.transform.Find("Background").GetComponent<Image>();
        background.color = new Color(fillColor.r * 0.5f, fillColor.g * 0.5f, fillColor.b * 0.5f, 1);

        return slider;
    }

    TextMeshProUGUI CreateText(GameObject parent, string name, string defaultText)
    {
        GameObject textObj = Instantiate(textPrefab, parent.transform);
        textObj.name = name;
        TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
        tmp.text = defaultText;
        tmp.fontSize = 36;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;
        return tmp;
    }

    void SetRectTransform(RectTransform rect, Vector2 size, Vector2 anchor, Vector2 pivot)
    {
        rect.sizeDelta = size;
        rect.anchorMin = anchor;
        rect.anchorMax = anchor;
        rect.pivot = pivot;
    }
} 