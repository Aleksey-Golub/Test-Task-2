using System;
using UnityEngine;
using DG.Tweening;

public enum CardParameter
{
    Mana,
    Health,
    Attack
}

public class Card : MonoBehaviour
{
    [SerializeField] private CardView _view;

    private CardData _data;
    private int _mana;
    private int _health;
    private int _attack;
    private bool _isDied => _health < 1;

    public event Action<Card> Died;

    public void Init(CardData data)
    {
        _data = data;
        _mana = data.Mana;
        _health = data.Health;
        _attack = data.Attack;

        _view.Init(data);
    }

    public void SetPosition(Vector3 newPosition, float duration)
    {
        transform.DOMove(newPosition, duration);
    }

    public void SetRotationZ(float zRotation)
    {
        transform.eulerAngles = new Vector3(0, 0, zRotation);
    }

    public void SetParameter(CardParameter parameterType, int newValue)
    {
        switch (parameterType)
        {
            case CardParameter.Mana:
                _view.DisplayMana(_mana, newValue);
                _mana = newValue;
                break;
            case CardParameter.Health:
                _view.DisplayHealth(_health, newValue);

                _health = newValue;
                if (_isDied)
                    Die();
                break;
            case CardParameter.Attack:
                _view.DisplayAttack(_attack, newValue);
                _attack = newValue;
                break;
            default:
                break;
        }
    }

    private void Die()
    {
        Died?.Invoke(this);
    }
}
