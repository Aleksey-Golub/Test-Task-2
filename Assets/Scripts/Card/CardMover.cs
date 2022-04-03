using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class CardMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _returnToHandDuration = 0.2f;
    [SerializeField] private Image _body;

    private Vector3 _offset;
    private Vector3 _oldPosition;
    private Vector3 _oldRotation;

    public event Action<CardState> StateChanged;

    public void SetPosition(in Vector3 newPosition, float duration)
    {
        transform.DOMove(newPosition, duration);
    }

    public void SetRotation(in Vector3 euler)
    {
        transform.eulerAngles = euler;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _body.raycastTarget = false;

        _oldPosition = transform.position;
        _oldRotation = transform.eulerAngles;

        _offset = transform.position - new Vector3(eventData.position.x, eventData.position.y);
        SetRotation(Vector3.zero);

        StateChanged?.Invoke(CardState.Drag);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x, eventData.position.y) + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter && eventData.pointerEnter.TryGetComponent(out PlayerDropPanel dropPanel))
        {
            transform.parent = dropPanel.transform;
            StateChanged?.Invoke(CardState.DropOnPanel);
        }
        else
        {
            SetPosition(_oldPosition, _returnToHandDuration);
            SetRotation(_oldRotation);

            StateChanged?.Invoke(CardState.InHand);
            _body.raycastTarget = true;
        }
    }
}
