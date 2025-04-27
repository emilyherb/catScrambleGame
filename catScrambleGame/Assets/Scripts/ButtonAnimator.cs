using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    private Vector3 hoverScale;
    private Vector3 pressedScale;
    private float animationSpeed = 10f;

    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        hoverScale = originalScale * 1.1f;
        pressedScale = originalScale * 0.9f;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, animationSpeed * Time.unscaledDeltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetScale = hoverScale;
    }
}
