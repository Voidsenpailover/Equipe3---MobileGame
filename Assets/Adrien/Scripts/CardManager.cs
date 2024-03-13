using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<CardData> _cards;

    public CardData Card;
    public CardData Card2;
    
    [Header("Card1")]
    [SerializeField] private GameObject _cardPanel;
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardGoodEffect;
    [SerializeField] private TextMeshProUGUI _cardBadEffect;
    [SerializeField] private Image _cardIcone;
    [SerializeField] private Image _cardBackground;
    [Header("Card2")]
    [SerializeField] private GameObject _cardPanel2;
    [SerializeField] private TextMeshProUGUI _cardName2;
    [SerializeField] private TextMeshProUGUI _cardGoodEffect2;
    [SerializeField] private TextMeshProUGUI _cardBadEffect2;
    [SerializeField] private Image _cardIcone2;
    [SerializeField] private Image _cardBackground2;

    public static event Action<CardData> CardSelected;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.left);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == _cardPanel)
                {
                    CardSelected?.Invoke(Card);
                    _cardPanel.SetActive(false);
                    _cardPanel2.SetActive(false);
                    Time.timeScale = 1; 
                }
                else if (hit.collider.gameObject == _cardPanel2)
                {
                    CardSelected?.Invoke(Card2);
                    _cardPanel.SetActive(false);
                    _cardPanel2.SetActive(false);
                    Time.timeScale = 1; 
                }
            }
        }
    }
    
    

    private void OnEnable()
    {
        EnemySpawner.CardChoice += ChooseCard;
    }
    
    private void OnDestroy()
    {
        EnemySpawner.CardChoice -= ChooseCard;
    }
    
    
    private void ChooseCard()
    {
        var card = _cards[Random.Range(0, _cards.Count)];
        var card2 = _cards[Random.Range(0, _cards.Count)];
        InitializeCard(card, card2);
        _cards.Remove(card);
        _cards.Remove(card2);
        _cardPanel.SetActive(true);
        _cardPanel2.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void InitializeCard(CardData card, CardData card2)
    {
        Card = card;
        Card2 = card2;
        _cardName.text = card.Name;
        _cardGoodEffect.text = card.GoodEffect;
        _cardBadEffect.text = card.BadEffect;
        _cardIcone.sprite = card.Icone;
        _cardBackground.sprite = card.Background;
        _cardName2.text = card2.Name;
        _cardGoodEffect2.text = card2.GoodEffect;
        _cardBadEffect2.text = card2.BadEffect;
        _cardIcone2.sprite = card2.Icone;
        _cardBackground2.sprite = card2.Background;
    }
}
