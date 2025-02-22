using UnityEngine;
using UnityEngine.EventSystems;  // Needed for IPointerDownHandler

public class uibuttonanimationcta : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buttonRect; // The main button
    public RectTransform triangleLeft; // Left triangle
    public RectTransform triangleRight; // Right triangle
    public CanvasGroup triangleLeftGroup; // CanvasGroup for fading
    public CanvasGroup triangleRightGroup; // CanvasGroup for fading
    private Vector3 triangleLeftInitialPosition;
    private Vector3 triangleRightInitialPosition;
    public float animationDuration = 0.2f; // Speed of animations
    public float triangleMoveDistance = 10f; // Distance triangles move outward
    public float trianglePressedDistance = 5f; // Distance triangles move when pressed
    private bool isPressed = false;
    private bool isHovered = false;

    void Start()
    {
        // Ensure triangles start hidden
        triangleLeftGroup.alpha = 0;
        triangleRightGroup.alpha = 0;
        triangleLeft.gameObject.SetActive(false);
        triangleRight.gameObject.SetActive(false);

        // Store the initial position as Vector3
        triangleLeftInitialPosition = triangleLeft.localPosition;
        triangleRightInitialPosition = triangleRight.localPosition;
    }

    // Called when the mouse button is pressed down on the UI element
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;

        // Move triangles to their pressed position
        LeanTween.moveLocalX(triangleLeft.gameObject, triangleLeftInitialPosition.x - trianglePressedDistance, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalX(triangleRight.gameObject, triangleRightInitialPosition.x + trianglePressedDistance, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
    }

    // Called when the mouse button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;

        // Move triangles back to their original position when the button is released
        LeanTween.moveLocalX(triangleLeft.gameObject, triangleLeftInitialPosition.x - triangleMoveDistance, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalX(triangleRight.gameObject, triangleRightInitialPosition.x + triangleMoveDistance, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);

        // If still hovered, keep the hover state, otherwise reset
        LeanTween.scale(buttonRect.gameObject, isHovered ? new Vector3(1.05f, 1.05f, 1f) : Vector3.one, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);

        if (!isHovered)
        {
            // Move triangles back and fade them out
            LeanTween.moveLocalX(triangleLeft.gameObject, triangleLeftInitialPosition.x + triangleMoveDistance, animationDuration)
                     .setEase(LeanTweenType.easeOutQuad);
            LeanTween.moveLocalX(triangleRight.gameObject, triangleRightInitialPosition.x - triangleMoveDistance, animationDuration)
                     .setEase(LeanTweenType.easeOutQuad);

            LeanTween.alphaCanvas(triangleLeftGroup, 0f, animationDuration).setOnComplete(() =>
            {
                triangleLeft.gameObject.SetActive(false);
            });

            LeanTween.alphaCanvas(triangleRightGroup, 0f, animationDuration).setOnComplete(() =>
            {
                triangleRight.gameObject.SetActive(false);
            });
        }
    }

    // Called when the mouse enters the button (hover effect)
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;

        // Slightly enlarge the button on hover
        LeanTween.scale(buttonRect.gameObject, new Vector3(1.05f, 1.05f, 1f), animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);

        // Show triangles and move them outward
        triangleLeft.gameObject.SetActive(true);
        triangleRight.gameObject.SetActive(true);

        LeanTween.alphaCanvas(triangleLeftGroup, 1f, animationDuration);
        LeanTween.alphaCanvas(triangleRightGroup, 1f, animationDuration);

        LeanTween.moveLocalX(triangleLeft.gameObject, triangleLeftInitialPosition.x - triangleMoveDistance, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalX(triangleRight.gameObject, triangleRightInitialPosition.x + triangleMoveDistance, animationDuration)
                 .setEase(LeanTweenType.easeOutQuad);
    }

    // Called when the mouse exits the button (hover effect)
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;

        if (!isPressed)
        {
            // Reset button size
            LeanTween.scale(buttonRect.gameObject, Vector3.one, animationDuration)
                     .setEase(LeanTweenType.easeOutQuad);

            // Move triangles back to their original position
            LeanTween.moveLocalX(triangleLeft.gameObject, triangleLeftInitialPosition.x, animationDuration)
                     .setEase(LeanTweenType.easeOutQuad);
            LeanTween.moveLocalX(triangleRight.gameObject, triangleRightInitialPosition.x - triangleMoveDistance, animationDuration)
                     .setEase(LeanTweenType.easeOutQuad);

            // Fade out triangles
            LeanTween.alphaCanvas(triangleLeftGroup, 0f, animationDuration).setOnComplete(() =>
            {
                triangleLeft.gameObject.SetActive(false);
            });

            LeanTween.alphaCanvas(triangleRightGroup, 0f, animationDuration).setOnComplete(() =>
            {
                triangleRight.gameObject.SetActive(false);
            });
        }
    }
}

