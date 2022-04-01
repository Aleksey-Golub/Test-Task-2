using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    [SerializeField] private List<CardData> _cards;

    public IReadOnlyList<CardData> Cards => _cards;

    public void AddCard(CardData newCard)
    {
        _cards.Add(newCard);
    }

    public void RemoveCard(CardData card)
    {
        _cards.Remove(card);
    }
}
