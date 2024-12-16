using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrowButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Vector3 arrowOffset = new Vector3(50, 0, 0); // Offset to adjust how far the arrows move

    private Vector3 leftArrowStartPos;
    private Vector3 rightArrowStartPos;

    private void Start()
    {
        // Store the initial positions of the arrows
        leftArrowStartPos = leftArrow.transform.position;
        rightArrowStartPos = rightArrow.transform.position;
    }

    // This is called when the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Teleport the arrows to the new position when hovering over the button
        TeleportArrows(leftArrowStartPos - arrowOffset, rightArrowStartPos + arrowOffset);
    }

    // This is called when the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Teleport the arrows back to their original positions when the mouse leaves
        TeleportArrows(leftArrowStartPos, rightArrowStartPos);
    }

    // Function to teleport arrows to their new positions
    private void TeleportArrows(Vector3 leftTarget, Vector3 rightTarget)
    {
        leftArrow.transform.position = leftTarget;
        rightArrow.transform.position = rightTarget;
    }
}
