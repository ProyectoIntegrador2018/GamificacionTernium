using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public Button item;
    public int level;
    public Image itemHolder;

    public Sprite candado;
    public Sprite itemDesbloqueable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(Database.getNivelJugador());
    }
}
