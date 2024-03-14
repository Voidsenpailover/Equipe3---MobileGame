using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimoire : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cards;
    private int _index;

    [SerializeField] private GameObject _boutonGauche;
    [SerializeField] private GameObject _boutonDroite;
    
    public void CloseGrimoire()
    {
        _index = 0;
        foreach (var card in _cards)
        {
            card.SetActive(false);
        }
        Time.timeScale = 1;
        _boutonGauche.SetActive(false);
        _boutonDroite.SetActive(false);
    }

    private void Update()
    {
        _boutonGauche.SetActive(_index != 0);
        _boutonDroite.SetActive(_index != _cards.Count - 1);
    }

    public void OpenGrimoire()
    {
        _cards[0].SetActive(true);
        _boutonDroite.SetActive(true);
        _index = 0;
        Time.timeScale = 0;
    }

    public void flecheGauche()
    {
        if (_index > 0)
        {
            _cards[_index].SetActive(false);
            _cards[_index - 1].SetActive(true);
            _index--;
        }
    }
    
    public void flecheDroite()
    {
        _cards[_index].SetActive(false);
        _cards[_index + 1].SetActive(true);
        _index++;
    }

    
    public void PageTourFeu()
    {
        _cards[_index].SetActive(false);
        _index = 3;
        _cards[3].SetActive(true);
    }
    
    public void PageTourFeu2()
    {
        _cards[_index].SetActive(false);
        _index = 4;
        _cards[4].SetActive(true);
    }
    
    public void PageTourFeu3()
    {
        _cards[_index].SetActive(false);
        _index = 5;
        _cards[5].SetActive(true);
    }
    
    public void PageTourTerre()
    {
        _cards[_index].SetActive(false);
        _index = 6;
        _cards[6].SetActive(true);
    } 
    
    public void PageTourTerre2()
    {
        _cards[_index].SetActive(false);
        _index = 7;
        _cards[7].SetActive(true);
    }
    
    public void PageTourTerre3()
    {
        _cards[_index].SetActive(false);
        _index = 8;
        _cards[8].SetActive(true);
    }
    
    public void PageTourEau()
    {
        _cards[_index].SetActive(false);
        _index = 9;
        _cards[9].SetActive(true);
    }
    
    public void PageTourEau2()
    {
        _cards[_index].SetActive(false);
        _index = 10;
        _cards[10].SetActive(true);
    }
    
    public void PageTourEau3()
    {
        _cards[_index].SetActive(false);
        _index = 11;
        _cards[11].SetActive(true);
    }
    
    
    public void PageTourVent()
    {
        _cards[_index].SetActive(false);
        _index = 12;
        _cards[12].SetActive(true);
    }
    
    public void PageTourVent2()
    {
        _cards[_index].SetActive(false);
        _index = 13;
        _cards[13].SetActive(true);
    }
    
    public void PageTourVent3()
    {
        _cards[_index].SetActive(false);
        _index = 14;
        _cards[14].SetActive(true);
    }
    

    public void PageTourMercure()
    {
        _cards[_index].SetActive(false);
        _index = 15;
        _cards[15].SetActive(true);
    }
    
    public void PageTourMercure2()
    {
        _cards[_index].SetActive(false);
        _index = 16;
        _cards[16].SetActive(true);
    }

    
    public void PageTourPhosphore()
    {
        _cards[_index].SetActive(false);
        _index = 17;
        _cards[17].SetActive(true);
    }
    
    public void PageTourPhosphore2()
    {
        _cards[_index].SetActive(false);
        _index = 18;
        _cards[18].SetActive(true);
    }
    public void PageTourPyrite()
    {
        _cards[_index].SetActive(false);
        _index = 19;
        _cards[19].SetActive(true);
    }
    
    
    public void PageTourPyrite2()
    {
        _cards[_index].SetActive(false);
        _index = 20;
        _cards[20].SetActive(true);
    }

    public void PageTourSel()
    {
        _cards[_index].SetActive(false);
        _index = 21;
        _cards[21].SetActive(true);
    }
    
    public void PageTourSel2()
    {
        _cards[_index].SetActive(false);
        _index = 22;
        _cards[22].SetActive(true);
    }
    
    
    public void PageTourSoufre()
    {
        _cards[_index].SetActive(false);
        _index = 23;
        _cards[23].SetActive(true);
    }

    public void PageTourSoufre2()
    {
        _cards[_index].SetActive(false);
        _index = 24;
        _cards[24].SetActive(true);
    }
    
    public void PageTourFulgurite()
    {
        _cards[_index].SetActive(false);
        _index = 25;
        _cards[25].SetActive(true);
    }

    public void PageTourFulgurite2()
    {
        _cards[_index].SetActive(false);
        _index = 26;
        _cards[26].SetActive(true);
    }
    
    public void Ennemi1()
    {
        _cards[_index].SetActive(false);
        _index = 27;
        _cards[27].SetActive(true);
    }
    
    public void EnnemiTypes()
    {
        _cards[_index].SetActive(false);
        _index = 28;
        _cards[28].SetActive(true);
    }

    public void Boss()
    {
        _cards[_index].SetActive(false);
        _index = 29;
        _cards[29].SetActive(true);
    }
}
