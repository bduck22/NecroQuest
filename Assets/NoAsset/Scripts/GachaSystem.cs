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
<<<<<<< Updated upstream
=======
    public Button clearButton;
    public Image image;
    public int Price;
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

    public Vector3 spawnPosition = Vector3.zero;

    private List<Sprite> characterPool = new List<Sprite>();
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    private GameObject currentCharacter;

    void Start()
    {
        characterPool.AddRange(characterPrefabs);
        gachaButton.onClick.AddListener(PullCharacter);
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        resetButton.onClick.AddListener(ResetPool);
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }

    public void PullCharacter()
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        ClearSpawnedCharacters();
        
        if (currentCharacter != null)
            currentCharacter.SetActive(false);
        
=======
        if(Data.Gold < Price)
=======
        if(Data.Gold < Price)
        {
            return;
        }

        if (characterPool.Count == 0)
>>>>>>> Stashed changes
        {
            return;
        }
<<<<<<< Updated upstream

>>>>>>> Stashed changes
        if (characterPool.Count == 0)
            return;
<<<<<<< Updated upstream

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
        
=======
        }
        image.gameObject.SetActive(true);
        int index = Random.Range(0, characterPool.Count);

        image.sprite = characterPool[index];
        image.transform.localPosition = spawnPosition;
        //image.transform.GetChild(0).GetComponent<Text>().text = Data.UnitData[];
        
        RectTransform rect = image.GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;

=======
        image.gameObject.SetActive(true);
        int index = Random.Range(0, characterPool.Count);

        image.sprite = characterPool[index];
        image.transform.localPosition = spawnPosition;
        //image.transform.GetChild(0).GetComponent<Text>().text = Data.UnitData[];
        
        RectTransform rect = image.GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;

>>>>>>> Stashed changes
        CanvasGroup cg = image.GetComponent<CanvasGroup>();
        if (cg == null) cg = image.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        rect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        cg.DOFade(1f, 0.5f);

<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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

    public void ResetPool()
    {
        characterPool.Clear();
        characterPool.AddRange(characterPrefabs);
<<<<<<< Updated upstream
=======
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
>>>>>>> Stashed changes
    }
}
