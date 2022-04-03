using System;
using UnityEngine;

public enum CardParameter
{
    Mana,
    Health,
    Attack
}

public enum CardState
{
    InHand,
    Drag,
    DropOnPanel    
}

public class Card : MonoBehaviour
{
    [SerializeField] private CardView _view;
    [SerializeField] private CardMover _mover;

    private CardData _data;
    private int _mana;
    private int _health;
    private int _attack;
    private bool _isDied => _health < 1;

    public event Action<Card> Died;
    public event Action<Card> DropedOnPanel;

    public void Init(CardData data)
    {
        _data = data;
        _mana = data.Mana;
        _health = data.Health;
        _attack = data.Attack;

        _mover.StateChanged += OnMoverStateChanged;
        _view.Init(data);
    }
    
    public void SetPosition(in Vector3 newPosition, float duration)
    {
        _mover.SetPosition(newPosition, duration);
    }

    public void SetRotation(in Vector3 euler)
    {
        _mover.SetRotation(euler);
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

    private void OnMoverStateChanged(CardState newState)
    {
        switch (newState)
        {
            case CardState.Drag:
                _view.ToggleGlow(true);
                break;
            case CardState.InHand:
                _view.ToggleGlow(false);
                break;
            case CardState.DropOnPanel:
                _view.ToggleGlow(false);
                DropedOnPanel?.Invoke(this);
                break;
            default:
                break;
        }
    }
}
