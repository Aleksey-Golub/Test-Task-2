using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHand _playerHand;
    [SerializeField] private CardFactory _cardFactory;
    [SerializeField] private PlayerDeck _playerDeck;
    [SerializeField] private PlayerHandCardsChanger _changer;
    [SerializeField] private Image _mediatorImage;
    [Header("Settings")]
    [Tooltip("Start count of cards in player hand")]
    [SerializeField] private Vector2Int _startCardCount = new Vector2Int(4, 6);
    [SerializeField] private string URL = "https://picsum.photos/200";
    [SerializeField] private CardData _warriorCardData;

    private TextureLoader _loader;

    private IEnumerator Start()
    {
        _loader = new TextureLoader();
        yield return StartCoroutine(_loader.DownloadImage(URL, _mediatorImage));
        _warriorCardData.Art = _mediatorImage.sprite;

        DistributeCards();
        _changer.Init(_playerHand);
        _playerHand.Init(_cardFactory);
    }

    private void DistributeCards()
    {
        int playerCardCount = Random.Range(_startCardCount.x, _startCardCount.y + 1);

        for (int i = 0; i < playerCardCount; i++)
        {
            int index = Random.Range(0, _playerDeck.Cards.Count);

            var newCard = _cardFactory.GetCard(_playerHand.transform);
            newCard.Init(_playerDeck.Cards[index]);

            _playerDeck.RemoveCard(_playerDeck.Cards[index]);

            _playerHand.AddCard(newCard);
        }
    }
}
