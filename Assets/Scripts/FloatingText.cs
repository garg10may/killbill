using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fadeSpeed = 1f;
    private TextMeshProUGUI text;
    private Color startColor;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        startColor = text.color;
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        text.color = new Color(startColor.r, startColor.g, startColor.b, 
            text.color.a - fadeSpeed * Time.deltaTime);
    }

    public static FloatingText Create(GameObject prefab, Vector3 position, string text, Color color)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        FloatingText floatingText = obj.GetComponent<FloatingText>();
        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.color = color;
        return floatingText;
    }
} 