using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioElement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IDragHandler, ISelectHandler, IMoveHandler, IDeselectHandler, IPointerExitHandler
{
    [SerializeField][Range(0, 2)] protected int clickPitch = 1;
    [Tooltip("When detecting movement from/to this element, which directions should trigger a click sound")]
    [SerializeField]
#if UNITY_EDIOR
        [EnumFlags]
#endif
    MoveDirectionMask clickDirections;
    [System.Flags]
    public enum MoveDirectionMask
    {
        Up = 0x01,
        Down = 0x02,
        Left = 0x04,
        Right = 0x08
    }

    public virtual void PlayHover()
    {
        GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.UI_Hover, 1, 1);
    }

    public virtual void PlayClick()
    {
        PlayClick(clickPitch);
    }

    public virtual void PlayClick(int pitch)
    {
        GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.UI_Click, 1, pitch);
    }

    public virtual void PlayBlock()
    {
        GerenciadorDeSFX.instancia.TocaSFX(GerenciadorDeSFX.Efeitos.UI_Block, 1, 1);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover");
        PlayHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        Button button = GetComponent<Button>();
        if (button && !button.interactable)
            PlayBlock();
        else
            PlayClick(clickPitch);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        PlayHover();
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        PlayHover();
    }

    public void OnDeselect(BaseEventData eventData)
    {
    }

    public virtual void OnMove(AxisEventData eventData)
    {
        int convertedDirection = ConvertDirection(eventData.moveDir);
        if (ShouldPlaySound(convertedDirection))
            PlayClick(clickPitch);
    }

    bool ShouldPlaySound(int moveDirection)
    {
        return ((int)clickDirections & moveDirection) == moveDirection;
    }

    private int ConvertDirection(MoveDirection moveDir)
    {
        switch (moveDir)
        {
            case MoveDirection.Left:
                return (int)MoveDirectionMask.Left;
            case MoveDirection.Up:
                return (int)MoveDirectionMask.Up;
            case MoveDirection.Right:
                return (int)MoveDirectionMask.Right;
            case MoveDirection.Down:
                return (int)MoveDirectionMask.Down;
            case MoveDirection.None:
            default:
                return 0;
        }
    }
}