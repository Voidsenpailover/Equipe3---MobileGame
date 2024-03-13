using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Card-", menuName = "Scriptable Objects/Card", order = 2)]
public class CardData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _goodeffect;
    [SerializeField] private string _badEffect;
    [SerializeField] private CardName _cardName;
    [SerializeField] private CardType _type;
    [SerializeField] private Sprite _icone;
    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _bouleSoleilLune;
    
    public string Name { get => _name; set => _name = value; }
    public string GoodEffect { get => _goodeffect; set => _goodeffect = value; }
    public string BadEffect { get => _badEffect; set => _badEffect = value; }
    public CardName CardName { get => _cardName; set => _cardName = value; }
    public CardType Type { get => _type; set => _type = value; }
    public Sprite Icone { get => _icone; set => _icone = value; }
    public Sprite Background { get => _background; set => _background = value; }
    public Sprite BouleSoleilLune { get => _bouleSoleilLune; set => _bouleSoleilLune = value; }
}
