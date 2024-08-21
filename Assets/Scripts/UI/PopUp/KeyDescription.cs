using UnityEngine;

public class KeyDescription : MonoBehaviour
{
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(710, -515);
    }
}
