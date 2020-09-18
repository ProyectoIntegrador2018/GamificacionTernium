using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Clase para el espacio donde se deposita un objeto
public class InstructionItemSlot : MonoBehaviour, IDropHandler {

    // Funcion donde identifica cuando se deja caer un objto en el espacio
    public void OnDrop(PointerEventData eventData) {
    	eventData.pointerDrag.GetComponent<InstructionDragDrop>().droppedOnSlot = true;
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null) {
        	Vector3 position = GetComponent<RectTransform>().anchoredPosition;
        	position.y += InstructionGV.sumPos;
            droppedObject.GetComponent<RectTransform>().anchoredPosition = position;
            InstructionGV.sumPos = InstructionGV.sumPos - 90;
        }
    }
}