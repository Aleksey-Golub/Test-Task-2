using System.Collections.Generic;
using UnityEngine;

public class PlayerHandView : MonoBehaviour
{
    [SerializeField] private RectTransform _leftPoint;
    [SerializeField] private RectTransform _rightPoint;
    [Header("Settings")]
    [SerializeField] private float _desiredStep = 0.2f;
    [SerializeField] private float _rotationStep = 5f;
    [SerializeField] private float _cardMoveDuration = 0.3f;
    [SerializeField] private Vector3 _arcZenith = new Vector3(0, 100, 0);
    [SerializeField] private AnimationCurve _curve;

    public void Place(IReadOnlyList<Card> cards)
    {
        float step = 1f / cards.Count;
        step = Mathf.Clamp(step, 0, _desiredStep);

        float t = (1 - step * (cards.Count - 1)) * 0.5f;

        int middleIndex = (cards.Count - 1) / 2;

        for (int i = 0; i < cards.Count; i++)
        {
            Vector3 newPosition = _arcZenith * _curve.Evaluate(t * 2) + Vector3.Lerp(_leftPoint.position, _rightPoint.position, t);
            cards[i].SetPosition(newPosition, _cardMoveDuration);

            float zRotation = (middleIndex - i) * _rotationStep;
            cards[i].SetRotation(new Vector3(0, 0, zRotation));

            t += step;
        }
    }
}
