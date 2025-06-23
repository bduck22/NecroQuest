using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class GachaSystem : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform uiParentCanvas;
    public Button gachaButton;
    
    public GameObject magicCirclePrefab;
    public Vector3 spawnPosition = Vector3.zero;
    
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
    
            GameObject magicCircle = Instantiate(magicCirclePrefab, uiParentCanvas);
            magicCircle.transform.localPosition = spawnPosition;
            magicCircle.transform.localScale = Vector3.zero;
    
            CanvasGroup circleGroup = magicCircle.GetComponent<CanvasGroup>();
            if (circleGroup == null) circleGroup = magicCircle.AddComponent<CanvasGroup>();
            circleGroup.alpha = 0;
    
            Sequence gachaSeq = DOTween.Sequence();
            
            gachaSeq.Append(magicCircle.transform.DOScale(1.2f, 0.4f).SetEase(Ease.OutBack));
            gachaSeq.Join(circleGroup.DOFade(1f, 0.4f));
            gachaSeq.AppendInterval(0.3f);
            
            gachaSeq.AppendCallback(() =>
            {
                GameObject character = Instantiate(characterPool[index], uiParentCanvas);
                character.transform.localPosition = spawnPosition;
    
                RectTransform rect = character.GetComponent<RectTransform>();
                rect.localScale = Vector3.zero;
    
                CanvasGroup cg = character.GetComponent<CanvasGroup>();
                if (cg == null) cg = character.AddComponent<CanvasGroup>();
                cg.alpha = 0f;
    
                rect.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                cg.DOFade(1f, 0.5f);
    
                characterPool.RemoveAt(index);
            });
            
            gachaSeq.AppendInterval(1f);
            gachaSeq.AppendCallback(() => Destroy(magicCircle));
        }
}
