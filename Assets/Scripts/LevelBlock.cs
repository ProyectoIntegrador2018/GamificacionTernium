using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public Button item; // item que se va a bloquear
    public int level; // numero de nivel en el que el item se va a desbloquear
    public Image itemHolder; // imagen del item

    public Sprite candado; // sprite del item bloqueado
    public Sprite itemDesbloqueable; // sprite del item desbloqueado
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // si el nivel del usuario es menor al nivel el item permanece bloqueado
        if(Database.getNivelJugador()< level)
        {
            item.interactable = false;
            itemHolder.sprite = candado;
        }
        else
        {
            item.interactable = true;
            itemHolder.sprite = itemDesbloqueable;
        }
       // Debug.Log(Database.getNivelJugador());
    }
}
