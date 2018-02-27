using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour, IPointerEnterHandler/*, ISelectHandler, IDeselectHandler, IPointerExitHandler*/
{
	public SoundButtonData sounds;

	private void Start() 
	{
		Button button = GetComponent<Button>();

		button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		if(GlobalSoundEffects.Instance != null)
			GlobalSoundEffects.Instance.Play(sounds.clickedClip);
	}

    // public void OnSelect(BaseEventData eventData)
    // {
    //     if(GlobalSoundEffects.Instance != null)
	// 		GlobalSoundEffects.Instance.Play(sounds.selectedClip);
    // }

	public void OnPointerEnter(PointerEventData eventData)
    {
        if(GlobalSoundEffects.Instance != null)
			GlobalSoundEffects.Instance.Play(sounds.selectedClip);
    }

    // public void OnDeselect(BaseEventData eventData)
    // {
    //     if(GlobalSoundEffects.Instance != null)
	// 		GlobalSoundEffects.Instance.Play(sounds.deselectedClip);
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     if(GlobalSoundEffects.Instance != null)
	// 		GlobalSoundEffects.Instance.Play(sounds.deselectedClip);
    // }
}
