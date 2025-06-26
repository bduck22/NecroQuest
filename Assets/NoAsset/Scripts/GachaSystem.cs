using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class GachaSystem : MonoBehaviour
{
    public Sprite[] characterPrefabs;
    public Transform uiParentCanvas;
    public Button gachaButton;
    public Button resetButton;
    public Button clearButton;
    public Image image;
    public int Price;

    public Vector3 spawnPosition = Vector3.zero;

    public List<int> characterPool = new List<int>();

    void Start()
    {
        gachaButton.onClick.AddListener(PullCharacter);
        for (int j = 0; j < 7; j++)
        {
            characterPool.Add(j);
        }
        foreach (int i in Data.Units)
        {
            characterPool.Remove(i);
        }
    }

    public void PullCharacter()
    {
        if (characterPool.Count <= 0)
        {
            LobbyManager.Instance.Wanning(Wannings.Unit);
            return;
        }

        if (!LobbyManager.Instance.UseMoney(Price))
        {
            return;
        }

        image.gameObject.SetActive(true);
        int index = UnityEngine.Random.Range(0, characterPool.Count);

        image.sprite = characterPrefabs[characterPool[index]];
        image.transform.localPosition = spawnPosition;
        image.transform.GetComponentInChildren<Text>().text = Data.UnitData[(UnitClass)characterPool[index]].Name;
        
        RectTransform rect = image.GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;

        CanvasGroup cg = image.GetComponent<CanvasGroup>();
        if (cg == null) cg = image.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        rect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        cg.DOFade(1f, 0.5f);

        LobbyManager.Instance.UnitAdd(characterPool[index]);
        characterPool.Remove(characterPool[index]);
    }
}
