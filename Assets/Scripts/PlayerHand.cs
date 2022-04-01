using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;
    [SerializeField] private PlayerHandView _view;

    private ICardRecycler _recycler;

    public IReadOnlyList<Card> Cards => _cards;

    public void Init(ICardRecycler recycler)
    {
        _recycler = recycler;
    }

    public void AddCard(Card newCard)
    {
        _cards.Add(newCard);
        newCard.Died += OnCardDied;

        _view.Place(_cards);
    }

    private void RemoveCard(Card card)
    {
        _cards.Remove(card);
        card.Died -= OnCardDied;
        _view.Place(_cards);
    }
    
    public void ChangeCard(int cardIndex, CardParameter parameter, int newValue)
    {
        _cards[cardIndex].SetParameter(parameter, newValue);
    }

    private void OnCardDied(Card card)
    {
        RemoveCard(card);
        _recycler.Recycle(card);
    }
}
