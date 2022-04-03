using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandCardsChanger : MonoBehaviour
{
    [SerializeField] private Button _changingButton;
    [SerializeField] private Vector2Int _newValueRange = new Vector2Int(-2,9);

    private PlayerHand _playerHand;
    private int _targetCardIndex = 0;

    public void Init(PlayerHand playerHand)
    {
        _changingButton.onClick.AddListener(ChangePlayerHandCard);
        _playerHand = playerHand;
    }

    private void ChangePlayerHandCard()
    {
        int cardsCount = _playerHand.Cards.Count;
        if (cardsCount == 0)
        {
            _changingButton.interactable = false;
            return;
        }

        int parameterIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(CardParameter)).Length);
        int newValue = UnityEngine.Random.Range(_newValueRange.x, _newValueRange.y);

        _playerHand.ChangeCardParameter(_targetCardIndex, (CardParameter)parameterIndex, newValue);

        if (cardsCount == _playerHand.Cards.Count)
            _targetCardIndex++;

        if (_targetCardIndex >= _playerHand.Cards.Count)
            _targetCardIndex = 0;

        if (_playerHand.Cards.Count == 0)
            _changingButton.interactable = false;
    }
}
