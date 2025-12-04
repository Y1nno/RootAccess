using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public class HoverTextColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text targetText;
    public Color hoverColor = Color.red;
    public Color normalColor = Color.white;

    [Header("Transition Settings")]
    public float transitionTime = 0.25f; // How fast the fade happens

    private Coroutine colorRoutine;

    void Start()
    {
        if (targetText == null)
        {
            targetText = GetComponent<TMP_Text>();
            if (targetText == null)
            {
                Debug.LogError("Target Text not assigned to HoverTextColorChange script.");
            }
        }
        targetText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (colorRoutine != null) StopCoroutine(colorRoutine);
        colorRoutine = StartCoroutine(ChangeColor(targetText.color, hoverColor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (colorRoutine != null) StopCoroutine(colorRoutine);
        colorRoutine = StartCoroutine(ChangeColor(targetText.color, normalColor));
    }

    private IEnumerator ChangeColor(Color startColor, Color endColor)
    {
        float elapsed = 0f;

        while (elapsed < transitionTime)
        {
            elapsed += Time.deltaTime;
            targetText.color = Color.Lerp(startColor, endColor, elapsed / transitionTime);
            yield return null;
        }

        targetText.color = endColor;
    }
}
