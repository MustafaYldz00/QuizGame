using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _soruText;


    public Soru[] _sorular;
    private static List<Soru> _cevaplanmamýsSorular;
    private Soru _gecerliSoru;
    void Start()
    {
        if (_cevaplanmamýsSorular == null || _cevaplanmamýsSorular.Count == 0)
        {
            _cevaplanmamýsSorular = _sorular.ToList<Soru>();
        }
        RastgeleSoruSec();
        
    }

    void RastgeleSoruSec()
    {
        int randomSoruIndexi = Random.Range(0, _cevaplanmamýsSorular.Count);
        _gecerliSoru = _cevaplanmamýsSorular[randomSoruIndexi];
        _soruText.text = _gecerliSoru._soru;
    }

    IEnumerator SoruArasibekle()
    {
        _cevaplanmamýsSorular.Remove(_gecerliSoru);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void dogruButonaBasildiMi()
    {
        if (_gecerliSoru._dogruMu)
        {
            Debug.Log("Doðru Cevap");
        }
        else
        {
            Debug.Log("Yanlýþ Cevap");
        }

        StartCoroutine(SoruArasibekle());
    }
    public void yanlisButonaBasildiMi()
    {
        if (!_gecerliSoru._dogruMu)
        {
            Debug.Log("Doðru Cevap");
        }
        else
        {
            Debug.Log("Yanlýþ Cevap");
        }
        StartCoroutine(SoruArasibekle());
    }
}
