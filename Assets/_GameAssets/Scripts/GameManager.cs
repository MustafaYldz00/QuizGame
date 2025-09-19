using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _soruText;
    [SerializeField] private Text _trueText, _falseText;
    [SerializeField] private GameObject _dogruButton, _yanlisButton;

    public Soru[] _sorular;
    private static List<Soru> _cevaplanmamýsSorular;
    private Soru _gecerliSoru;
    private int _dogruAdedi, _yanlisAdedi;

    void Start()
    {
        if (_cevaplanmamýsSorular == null || _cevaplanmamýsSorular.Count == 0)
        {
            _cevaplanmamýsSorular = _sorular.ToList<Soru>();
        }

        _dogruAdedi = 0;
        _yanlisAdedi = 0;

        RastgeleSoruSec();
        
    }

    void RastgeleSoruSec()
    {
        _yanlisButton.GetComponent<RectTransform>().DOLocalMoveX(499f, 1f);
        _dogruButton.GetComponent<RectTransform>().DOLocalMoveX(-445f, 1f);

        int randomSoruIndexi = Random.Range(0, _cevaplanmamýsSorular.Count);
        _gecerliSoru = _cevaplanmamýsSorular[randomSoruIndexi];
        _soruText.text = _gecerliSoru._soru;

        if (_gecerliSoru._dogruMu)
        {
            _trueText.text = "DOÐRU CEVAPLADINIZ";
            _falseText.text = "YANLIÞ CEVAPLADINIZ";
        }else
        {
            _trueText.text = "YANLIÞ CEVAPLADINIZ";
            _falseText.text = "DOÐRU CEVAPLADINIZ";
        }
    }

    IEnumerator SoruArasibekle()
    {
        _cevaplanmamýsSorular.Remove(_gecerliSoru);
        
        yield return new WaitForSeconds(0.75f);
       
        if (_cevaplanmamýsSorular.Count <= 0)
        {
            Debug.Log("Doðru Sayýsý: "+ _dogruAdedi + " yanliþ sayýsý: "+ _yanlisAdedi);
        }else
        {
            RastgeleSoruSec();
        }
    }

    public void dogruButonaBasildiMi()
    {
        if (_gecerliSoru._dogruMu)
        {
            Debug.Log("Doðru Cevap");
            _dogruAdedi++;
        }
        else
        {
            Debug.Log("Yanlýþ Cevap");
            _yanlisAdedi++;
        }
        _yanlisButton.GetComponent<RectTransform>().DOLocalMoveX(1500f, 0.2f);

        StartCoroutine(SoruArasibekle());
    }
    public void yanlisButonaBasildiMi()
    {
        if (!_gecerliSoru._dogruMu)
        {
            Debug.Log("Doðru Cevap");
            _dogruAdedi++;
        }
        else
        {
            Debug.Log("Yanlýþ Cevap");
            _yanlisAdedi++;
        }
        _dogruButton.GetComponent<RectTransform>().DOLocalMoveX(-1500f, 0.2f);
       StartCoroutine(SoruArasibekle());
    }
}
