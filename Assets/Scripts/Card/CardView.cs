using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _art;
    [SerializeField] private Image _glow;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Text _mana;
    [SerializeField] private Text _health;
    [SerializeField] private Text _attack;
    [Header("Settings")]
    [SerializeField] private float _valueUpdateDuration = 0.3f;

    public void Init(CardData data)
    {
        _title.text = data.Title;
        _description.text = data.Description;
        DisplayMana(data.Mana, data.Mana);
        DisplayHealth(data.Health, data.Health);
        DisplayAttack(data.Attack, data.Attack);
        SetArt(data.Art);
    }

    public void ToggleGlow(bool state)
    {
        _glow.enabled = state;
    }

    public void SetArt(Sprite sprite)
    {
        _art.sprite = sprite;
    }

    public void DisplayMana(int oldValue, int newValue)
    {
        DisplayParameter(_mana, oldValue, newValue);
    }

    public void DisplayHealth(int oldValue, int newValue)
    {
        DisplayParameter(_health, oldValue, newValue);
    }

    public void DisplayAttack(int oldValue, int newValue)
    {
        DisplayParameter(_attack, oldValue, newValue);
    }

    private void DisplayParameter(Text text, int oldValue, int newValue)
    {
        int stepCount = Mathf.Abs(oldValue - newValue);
        bool increase = newValue >= oldValue;

        float step = _valueUpdateDuration / stepCount;

        Sequence sequence = DOTween.Sequence();

        if (stepCount == 0)
            text.DOText(newValue.ToString(), _valueUpdateDuration);

        for (int i = 0; i < stepCount; i++)
        {
            int stepValue = increase ? oldValue + (1 + i) : oldValue - (1 + i);

            sequence.Append(text.DOText(stepValue.ToString(), step));
        }
    }
}
