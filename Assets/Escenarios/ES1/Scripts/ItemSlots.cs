using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Clase para el espacio donde se deposita un objeto
public class ItemSlots : MonoBehaviour, IDropHandler {

    // Obtiene la etiqueta del objeto para que se pueda relacionar con su espacio designado
	int intTag;
    
    // Funcion donde identifica cuando se deja caer un objto en el espacio
	public void OnDrop(PointerEventData eventData) {
    	eventData.pointerDrag.GetComponent<DragDrops>().droppedOnSlot = true;
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            intTag = int.Parse(this.gameObject.tag);
	    	Debug.Log(intTag);
	    	GlobalVariables.currentTagItem = intTag;
        }
    }
}