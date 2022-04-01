using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Create New Card")]
public class CardData : ScriptableObject
{
    [SerializeField] private Sprite _art;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private int _mana;
    [SerializeField] private int _health;
    [SerializeField] private int _attack;

    public Sprite Art { get => _art; set => _art = value; }
    public string Title => _title;
    public string Description => _description;
    public int Mana => _mana;
    public int Health => _health;
    public int Attack => _attack;

}
