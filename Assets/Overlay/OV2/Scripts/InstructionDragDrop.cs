using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.IO;

// Clase para los objetos que son arrastrables
public class InstructionDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

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
            if (InstructionGV.items.Count > 0) {
                InstructionGV.items.Remove(this.gameObject);
                Debug.Log("Removed");
            }
        }
        if (droppedOnSlot == false && itemWasHere == false) {
            Debug.Log("Out of area");
            rectTransform.anchoredPosition = defaultPos;  
        }
        if (droppedOnSlot == true) {
            Debug.Log("Inside of area");
            InstructionGV.items.Add(this.gameObject);
            Debug.Log("Added");
            itemWasHere = true;
        }
    }

    // Mouse presionado
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
    }

    // Estado que evalua si los objetos dentro del item slot son los correctos
    public static string statusAnswer() {
        Debug.Log(InstructionGV.items.Count);
        if (InstructionGV.items.Exists(x => x.name == "Item1") && 
            InstructionGV.items.Exists(x => x.name == "Item2") && 
            InstructionGV.items.Exists(x => x.name == "Item3") && 
            InstructionGV.items.Exists(x => x.name == "Item4") && 
            InstructionGV.items.Count == 4) {
            Debug.Log("Correct");
            return "Correct";
        } else {
            Debug.Log("Incorrect");
            return "Incorrect";
        }
    }
}

