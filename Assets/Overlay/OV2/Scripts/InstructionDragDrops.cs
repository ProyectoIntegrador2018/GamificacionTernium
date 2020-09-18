using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.IO;

// Clase para los objetos que son arrastrables
public class InstructionDragDrops : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    
    // Variables privadas
    [SerializeField] private Canvas canvas = null;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 defaultPos;
    private bool itemWasHere = false;

    // Variables publicas
    public bool droppedOnSlot = false;
    
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPos = GetComponent<RectTransform>().localPosition;
    }

    // Objeto se empieza a arrastrar
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        droppedOnSlot = false;
    }

    // Objeto siendo arrastrado
    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Objeto dejo de ser arrastrado
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (droppedOnSlot == false && itemWasHere == true) {
        	Debug.Log("Out of area");
        	rectTransform.anchoredPosition = defaultPos; 
        	InstructionGV.pairAnswerSlot.Remove(int.Parse(this.gameObject.tag));
        	Debug.Log("Count: " + InstructionGV.pairAnswerSlot.Count);
        }
        if (droppedOnSlot == false && itemWasHere == false) {
            Debug.Log("Out of area");
            rectTransform.anchoredPosition = defaultPos;
            Debug.Log("Count: " + InstructionGV.pairAnswerSlot.Count);
        }
        if (droppedOnSlot == true) {
        	Debug.Log("Inside of area");
        	itemWasHere = true;
        	int intTag = int.Parse(this.gameObject.tag);
        	Debug.Log(intTag);
        	InstructionGV.pairAnswerSlot.Add(intTag, InstructionGV.currentTagItem);
        	Debug.Log("Count: " + InstructionGV.pairAnswerSlot.Count);
        }
    }

    // Mouse presionado
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    // Estado que evalua si los objetos dentro del item slot son los correctos
    public static string statusAnswer() {
        foreach(KeyValuePair<int, int> x in InstructionGV.pairAnswerSlot) {
        	if (x.Key == x.Value) {
        		Debug.Log("Correct");
        	} else {
        		Debug.Log("Incorrect");
        		return "Incorrect";
        	}
        }
        return "Correct";
    }
}
