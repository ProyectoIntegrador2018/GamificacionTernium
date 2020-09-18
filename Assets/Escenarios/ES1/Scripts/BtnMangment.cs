using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Clase para manejar los botones en P2 y P6 - escenas que tienen funcionalidad de drag and drop
public class BtnMangment : MonoBehaviour
{
    // Variables publicas
    public Button Boton;
    public string SigEscena;
    public Text DialogueText;
    GameObject[] ListaDItems;
    float[] PosX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    float[] PosY = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int cuantosItems;
    string Item;
    string ItemBox;
    string ItemSlot;
    static int CH;

    private void Awake()
    {
        ContarItems();
        FindItems();
        Shuffle();
    }

    private void OnEnable()
    {
        Boton.onClick.AddListener(delegate 
        {
            // Si la escena en juego es la P2
            if (SceneManager.GetActiveScene().name == "P2" || SceneManager.GetActiveScene().name == "ES4P2") {
                if (DragDrop.statusAnswer() == "Correct") {
                DialogueText.text = "Correcto! El guardia ahora tiene su equipo de seguridad puesto.";
                // Suma puntos
                GameMind.addPoints(100);
                StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrop.statusAnswer() == "Incorrect") {
                    DialogueText.text = "Incorrecto! El guardia debe tener puesto su casco de seguridad con barbiquejo, lentes de seguridad, guantes combinados de carnaza y botines de seguridad con casquillo.";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
            }
            // Si la escena en juego es la P6
            if (SceneManager.GetActiveScene().name == "P6") {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 7) {
                DialogueText.text = "Correcto! El guardia siguió el orden adecuado y el rodillo será arreglado.";
                // Suma puntos
                GameMind.addPoints(100);
                StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 7) {
                    DialogueText.text = "Incorrecto! Te faltaron pasos, el orden correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 7) {
                    DialogueText.text = "Incorrecto! El orden correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 7) {
                    DialogueText.text = "Incorrecto! Te faltaron pasos y el orden correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }

            }
            // Si la escena en juego es la ES2P3
            if (SceneManager.GetActiveScene().name == "ES2P3")
            {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    DialogueText.text = "Correcto! Usaste tus sentidos de manera correcta.";
                    // Suma puntos
                    GameMind.addPoints(100);
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos, lo correcto sería ...";
                    Debug.Log(GlobalVariables.pairAnswerSlot.Count);
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    DialogueText.text = "Incorrecto! lo correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos y lo correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }

            }
            // Si la escena en juego es la ES2P5
            if (SceneManager.GetActiveScene().name == "ES2P5")
            {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    DialogueText.text = "Correcto! Seguiste el orden óptimo para el bloqueo";
                    // Suma puntos
                    GameMind.addPoints(100);
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos, lo correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    DialogueText.text = "Incorrecto! el orden correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos y el orden correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }

            }

             if (SceneManager.GetActiveScene().name == "ES3P3.1")
            {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    DialogueText.text = "Correcto!";
                    // Suma puntos
                    GameMind.addPoints(100);
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect")
                {
                    DialogueText.text = "Incorrecto! Posible daño permanente en el equipo.";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                   // GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else
                {
                    DialogueText.text = "Incorrecto! Posible daño permanente en el equipo.";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }

            }

            // INICIO - PARTE DE FABIANA
            // Si la escena en juego es la ES4P4
            if (SceneManager.GetActiveScene().name == "ES4P4") {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    DialogueText.text = "Correcto! Esos son los tipos de daño que podrían estar ocurriendo.";
                    // Suma puntos
                    GameMind.addPoints(100);
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos, lo correcto sería ...";
                    Debug.Log(GlobalVariables.pairAnswerSlot.Count);
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                   // GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 1)
                {
                    DialogueText.text = "Incorrecto! lo correcto sería B...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 1)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos y lo correcto sería C...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }

            }

            // Si la escena en juego es la ES4P5
            if (SceneManager.GetActiveScene().name == "ES4P5") {
                if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    DialogueText.text = "Correcto! Relacionaste de manera perfecta.";
                    // Suma puntos
                    GameMind.addPoints(100);
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Correct" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos, lo correcto sería ...";
                    Debug.Log(GlobalVariables.pairAnswerSlot.Count);
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count == 3)
                {
                    DialogueText.text = "Incorrecto! lo correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }
                else if (DragDrops.statusAnswer() == "Incorrect" && GlobalVariables.pairAnswerSlot.Count != 3)
                {
                    DialogueText.text = "Incorrecto! Te faltaron pasos y lo correcto sería ...";
                    // Quita vida y suma puntos
                    GameMind.takeAwayLive(1);
                    //GameMind.addPoints(-100);
                    Solution();
                    StartCoroutine(WaitSeconds(5));
                }

            }
            // FIN - PARTE DE FABIANA
        });
    }

    // Coroutine donde se espera 5 segundos para que el usuario pueda leer el feedback
    IEnumerator WaitSeconds(int seconds) {
        Boton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(seconds);
        // Si las vidas es 0 o menos se cargara la escena de perder, sino la siguiente escena
        if (GlobalVariables.lives <= 0)
        {
            string Escena = SceneManager.GetActiveScene().name;

            if (Escena.Substring(0, 1) == "P")
            {
                SceneManager.LoadScene("Lose");
            }
            else
            {
                SceneManager.LoadScene(Escena.Substring(0, 3) + "Lose");
            }

            //SceneManager.LoadScene("Lose");
        } else {
            SceneManager.LoadScene(SigEscena);
        }
    }

    //Ricky
    //Cambiar dew posicion las cosas
    public void Shuffle()
    {
        //CH = cuantosItems;
        int topRange = cuantosItems + 1;
        int Rand = Random.Range(1, topRange);
        for (int i = 1; i <= cuantosItems; i++)
        {
            while (PosX[Rand] == 0)
            {
                Rand = Random.Range(1, topRange);
            }
            ItemBox = "ItemBox" + i;
            GameObject Dummy = GameObject.Find(ItemBox);
            Dummy.transform.localPosition = new Vector2(PosX[Rand], PosY[Rand]);
            PosX[Rand] = 0;
            PosY[Rand] = 0;
        }
    }

    public void FindItems()
    { 
        for (int i = 1; i <= cuantosItems; i++)
        {
            ItemBox = "ItemBox" + i;
            GameObject Dummy = GameObject.Find(ItemBox) ;
            PosX[i] = Dummy.transform.localPosition.x;
            PosY[i] = Dummy.transform.localPosition.y;
        }
    }

    public void Solution()
    {
        string Escena = SceneManager.GetActiveScene().name;

        if (Escena == "P2" || Escena == "ES4P2")
        {
            GameObject Slot = GameObject.Find("ItemSlot");
            //Lentes
            GameObject Item1 = GameObject.Find("Item1");
            //Casco
            GameObject Item2 = GameObject.Find("Item2");
            //Boots
            GameObject Item3 = GameObject.Find("Item3");
            //Guantes
            GameObject Item4 = GameObject.Find("Item4");

            Item1.transform.localPosition = new Vector2(Slot.transform.localPosition.x, 94);
            Item2.transform.localPosition = new Vector2(Slot.transform.localPosition.x, 214);
            Item3.transform.localPosition = new Vector2(Slot.transform.localPosition.x, -146);
            Item4.transform.localPosition = new Vector2(Slot.transform.localPosition.x, -26);

        }
        else
        {
            for (int i = 1; i <= cuantosItems; i++)
            {
                //La cosa
                ItemBox = "ItemBox" + i;
                // Donde va
                ItemSlot = "ItemSlot" + i;

                GameObject Box = GameObject.Find(ItemBox);
                GameObject Slot = GameObject.Find(ItemSlot);

                if (Slot != null)
                {
                    Box.transform.localPosition = new Vector2(Slot.transform.localPosition.x, Slot.transform.localPosition.y);
                }
                else
                {
                    Box.transform.localPosition = new Vector2(-1000, -1000);
                }
            }
        }
    }

    public void ContarItems()
    {
        int i = 1;

        ItemBox = "ItemBox" + i;
        GameObject Box = GameObject.Find(ItemBox);

        while (Box != null)
        {
            i++;
            ItemBox = "ItemBox" + i;
            Box = GameObject.Find(ItemBox);
        }
        cuantosItems = i-1;
        CH = cuantosItems;
        //Debug.Log(cuantosItems);
    }

    public static void Help()
    {
        string Escena2 = SceneManager.GetActiveScene().name;

        if (Escena2 == "P2" || Escena2 == "ES4P2")
        {
            GameObject Slot = GameObject.Find("ItemSlot");
            //Lentes
            GameObject Item1 = GameObject.Find("Item1");
            //Casco
            GameObject Item2 = GameObject.Find("Item2");
            //Boots
            GameObject Item3 = GameObject.Find("Item3");
            //Guantes
            GameObject Item4 = GameObject.Find("Item4");

            if(Item1.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item1.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
            else if(Item2.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item2.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
            else if (Item3.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item3.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
            else if (Item4.transform.localPosition.x != Slot.transform.localPosition.x)
            {
                Item4.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
            }
  

        }
        else if(Escena2 == "P6")
        {
            for (int i = 1; i <= CH; i++)
            {
                //La cosa
                string ItemBox2 = "ItemBox" + i;
                // Donde va
                string ItemSlot2 = "ItemSlot" + i;

                GameObject Box = GameObject.Find(ItemBox2);
                GameObject Slot = GameObject.Find(ItemSlot2);

                if (Box.transform.localPosition.x != Slot.transform.localPosition.x)
                {
                    Box.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
                    Slot.GetComponent<Image>().color = new Color32(0, 179, 81, 255);
                    i = CH;
                }
            }
        }
        else
        {
            for (int i = 1; i <= CH; i++)
            {
                //La cosa
                string ItemBox2 = "ItemBox" + i;
                // Donde va
                string ItemSlot2 = "ItemSlot" + i;

                GameObject Box = GameObject.Find(ItemBox2);
                GameObject Slot = GameObject.Find(ItemSlot2);

                if (Slot == null)
                {
                    Box.SetActive(false);
                    i = CH;
                }
            }
        }
    }

}


