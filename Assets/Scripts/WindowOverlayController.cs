using UnityEngine;
using UnityEngine.EventSystems;

public class WindowOverlayController : MonoBehaviour, IPointerClickHandler
{
    public Animator windowAnimator;
    private static readonly int CloseModal = Animator.StringToHash("CloseModal");

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("overlay clicked");
        Close();
    }

    public void Close()
    {
        windowAnimator.SetTrigger(CloseModal);
        Debug.Log("overlay close");
    }
}
