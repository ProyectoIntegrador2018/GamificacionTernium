
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Clase para identificar los slots de los juegos de drag and drop
// Creada por el equipo 1

public class ItemSlotSingle : MonoBehaviour, IDropHandler
{
  public void OnDrop(PointerEventData eventData) {
    	eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        if (eventData.pointerDrag != null) {
        	Vector3 position = GetComponent<RectTransform>().anchoredPosition;
        	position.y += GlobalVariables.sumPos;
            droppedObject.GetComponent<RectTransform>().anchoredPosition = position;
        }
    }
}