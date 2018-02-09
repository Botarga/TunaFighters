using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public Transform parentToReturnTo = null;
	GameObject placeHolder = null;

	public void OnBeginDrag(PointerEventData eventData){
		placeHolder = new GameObject ();
		placeHolder.transform.SetParent (this.transform.parent);
		LayoutElement le = placeHolder.AddComponent<LayoutElement> ();
		le.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeHolder.transform.SetSiblingIndex (this.transform.GetSiblingIndex());

		parentToReturnTo = this.transform.parent;
		this.transform.SetParent (this.transform.root);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData){
		this.transform.position = eventData.position;

		int newSiblingIndex = parentToReturnTo.childCount;
		for(int i = 0; i < parentToReturnTo.childCount; ++i){
			if (this.transform.position.x < parentToReturnTo.GetChild (i).position.x) {
				newSiblingIndex = i;
				if (placeHolder.transform.GetSiblingIndex () < newSiblingIndex)
					newSiblingIndex--;
				break;
			}
		}

		placeHolder.transform.SetSiblingIndex (newSiblingIndex);
	}

	public void OnEndDrag(PointerEventData eventData){
		this.transform.SetParent(parentToReturnTo);
		this.transform.SetSiblingIndex (placeHolder.transform.GetSiblingIndex ());
		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		Destroy (placeHolder);
	}
}
