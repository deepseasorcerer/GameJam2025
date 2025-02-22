using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class uiButtonAnimationIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform buttonRect;
    public float hoverScale = 1.1f;
    public float pressScale = 0.9f;
    public float animationDuration = 0.15f;
    public AudioSource hoverSound;
    public AudioSource clickSound;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = buttonRect.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(buttonRect, originalScale * hoverScale, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
        hoverSound.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(buttonRect, originalScale, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.scale(buttonRect, originalScale * pressScale, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.scale(buttonRect, originalScale * hoverScale, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
        clickSound.Play();
    }
}
