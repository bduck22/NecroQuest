using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterIconUI : MonoBehaviour, IPointerClickHandler
{
    public int number;
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.root.GetComponent<AudioSource>().Play();
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            PlayerManager.instance.Units[number].transform.GetComponent<SpriteRenderer>().material = PlayerManager.instance.UnitManager.Select;
            PlayerManager.instance.SeletedUnits.Add(number);
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            PlayerManager.instance.ChaInfo(number);
        }
    }
}
