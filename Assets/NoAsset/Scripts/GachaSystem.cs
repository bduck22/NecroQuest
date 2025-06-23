using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class GachaSystem : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform uiParentCanvas;
    public Button gachaButton;
    public Button resetButton;
    public Button clearButton;

    public Vector3 spawnPosition = Vector3.zero;

    private List<GameObject> characterPool = new List<GameObject>();
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    void Start()
    {
        characterPool.AddRange(characterPrefabs);
        gachaButton.onClick.AddListener(PullCharacter);
        resetButton.onClick.AddListener(ResetPool);
        clearButton.onClick.AddListener(ClearSpawnedCharacters);
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
        character.transform.localPosition = spawnPosition;
        
        RectTransform rect = character.GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;

        CanvasGroup cg = character.GetComponent<CanvasGroup>();
        if (cg == null) cg = character.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        rect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        cg.DOFade(1f, 0.5f);

        spawnedCharacters.Add(character);
        characterPool.RemoveAt(index);
    }

    void ResetPool()
    {
        characterPool.Clear();
        characterPool.AddRange(characterPrefabs);
        Debug.Log("캐릭터 풀을 초기화했습니다.");
    }

    void ClearSpawnedCharacters()
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
