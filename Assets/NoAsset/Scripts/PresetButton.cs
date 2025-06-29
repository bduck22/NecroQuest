using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PresetButton : MonoBehaviour, IPointerClickHandler
{
    Preset preset;
    private void Start()
    {
        preset = transform.parent.parent.GetComponent<Preset>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        LobbyManager.Instance.GetComponent<AudioSource>().Play();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            preset.SetPre(int.Parse(transform.parent.name[transform.parent.name.Length - 1].ToString()) - 1);
            preset.SetSit(int.Parse(transform.name[transform.name.Length - 1].ToString()) - 1);
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            preset.SetPre(int.Parse(transform.parent.name[transform.parent.name.Length - 1].ToString()) - 1);
            preset.SetSit(int.Parse(transform.name[transform.name.Length - 1].ToString()) - 1);
            preset.Delete();
        }
    }
}
