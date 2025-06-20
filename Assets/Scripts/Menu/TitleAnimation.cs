using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    public float amplitude = 10f;     // Ne kadar yukarý-aþaðý sallanacak
    public float speedy = 1f;      // Ne kadar hýzlý sallanacak

    private RectTransform rectTransform;
    private Vector3 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speedy) * amplitude;
        rectTransform.anchoredPosition = startPos + new Vector3(0f, yOffset, 0f);
    }
}
