using UnityEngine;

[CreateAssetMenu(fileName = "New Card Factory", menuName = "Card Factory/Create New Factory")]
public class CardFactory : ScriptableObject, ICardRecycler
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
