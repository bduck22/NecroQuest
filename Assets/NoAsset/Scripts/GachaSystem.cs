using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class GachaSystem : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform uiParentCanvas;
    public Button gachaButton;
    
    private List<GameObject> characterPool = new List<GameObject>();
    
    void Start()
    {
        characterPool.AddRange(characterPrefabs);
        gachaButton.onClick.AddListener(PullCharacter);
    }

    void PullCharacter()
    {
        if (characterPool.Count == 0)
        {
            Debug.Log("모든 용병을 뽑았습니다!");
            return;
        }

        int index = Random.Range(0, characterPool.Count);

        GameObject character = Instantiate(characterPool[index], uiParentCanvas);
        character.transform.localPosition = Vector3.zero;

        RectTransform rect = character.GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;

        CanvasGroup cg = character.GetComponent<CanvasGroup>();
        if (cg == null) cg = character.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        rect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        cg.DOFade(1f, 0.5f);

        characterPool.RemoveAt(index);
    }
}
