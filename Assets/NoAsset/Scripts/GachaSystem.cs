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

    public Vector3 spawnPosition = Vector3.zero;

    private List<GameObject> characterPool = new List<GameObject>();
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    private GameObject currentCharacter;

    void Start()
    {
        characterPool.AddRange(characterPrefabs);
        gachaButton.onClick.AddListener(PullCharacter);
        resetButton.onClick.AddListener(ResetPool);
    }

    void PullCharacter()
    {
        ClearSpawnedCharacters();
        
        if (currentCharacter != null)
            currentCharacter.SetActive(false);
        
        if (characterPool.Count == 0)
            return;

        int index = Random.Range(0, characterPool.Count);

        currentCharacter = Instantiate(characterPool[index], uiParentCanvas);
        currentCharacter.transform.localPosition = spawnPosition;
        
        RectTransform rect = currentCharacter.GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;

        CanvasGroup cg = currentCharacter.GetComponent<CanvasGroup>();
        if (cg == null) cg = currentCharacter.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        rect.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
        cg.DOFade(1f, 0.4f);
        
        currentCharacter.SetActive(true);
        
        characterPool.RemoveAt(index);
    }
    
    public void ClearSpawnedCharacters()
    {
        foreach (var obj in spawnedCharacters)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        spawnedCharacters.Clear();
    }

    void ResetPool()
    {
        characterPool.Clear();
        characterPool.AddRange(characterPrefabs);
    }
}
