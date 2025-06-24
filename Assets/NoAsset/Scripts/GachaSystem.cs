using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

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

    private List<Sprite> characterPool = new List<Sprite>();
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    void Start()
    {
        characterPool.AddRange(characterPrefabs);
        gachaButton.onClick.AddListener(PullCharacter);
    }

    public void PullCharacter()
    {
        if(Data.Gold < Price)
        {
            return;
        }

        if (characterPool.Count == 0)
        {
            Debug.Log("모든 용병을 뽑았습니다!");
            return;
        }
        image.gameObject.SetActive(true);
        int index = Random.Range(0, characterPool.Count);

        image.sprite = characterPool[index];
        image.transform.localPosition = spawnPosition;
        //image.transform.GetChild(0).GetComponent<Text>().text = Data.UnitData[];
        
        RectTransform rect = image.GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;

        CanvasGroup cg = image.GetComponent<CanvasGroup>();
        if (cg == null) cg = image.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        rect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        cg.DOFade(1f, 0.5f);

        characterPool.RemoveAt(index);
    }

    public void ResetPool()
    {
        characterPool.Clear();
        characterPool.AddRange(characterPrefabs);
        Debug.Log("캐릭터 풀을 초기화했습니다.");
    }

    public void ClearSpawnedCharacters()
    {
        foreach (var obj in spawnedCharacters)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedCharacters.Clear();
        Debug.Log("생성된 캐릭터들을 삭제했습니다.");
    }
}
