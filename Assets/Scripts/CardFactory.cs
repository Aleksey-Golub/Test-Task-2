using UnityEngine;

public class CardFactory : MonoBehaviour, ICardRecycler
{
    [SerializeField] private Card _cardPrefab;

    public Card GetCard(Transform transform)
    {
        var newCard = Instantiate(_cardPrefab, transform);

        return newCard;
    }

    public void Recycle(Card card)
    {
        Destroy(card.gameObject);
    }
}
