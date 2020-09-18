using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Clase para el espacio donde se deposita un objeto
public class ItemSlot : MonoBehaviour, IDropHandler {

    // Funcion donde identifica cuando se deja caer un objto en el espacio
    public void OnDrop(PointerEventData eventData) {
    	eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null) {
        	Vector3 position = GetComponent<RectTransform>().anchoredPosition;
        	position.y += GlobalVariables.sumPos;
            droppedObject.GetComponent<RectTransform>().anchoredPosition = position;
            GlobalVariables.sumPos = GlobalVariables.sumPos - 120;
        }
    }
}
