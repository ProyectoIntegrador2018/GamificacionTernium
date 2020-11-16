using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AyudaManager : MonoBehaviour
{
    public GameObject Label;
    public GameObject backImg;
    public GameObject BA;
    User[] userList;

    // Start is called before the first frame update
    void Start()
    {
        userList = Database.GetUsers();
    }
    
    public void callNumerator(){
        Debug.Log("Llamando al mensaje");
        if (GameObject.Find("Canvas").GetComponent("BtnMangment") as BtnMangment != null)
        {
            
            BtnMangment.Help();
        }
        else
        {
            QuestionManager.Help();
        }
        StartCoroutine(DisplayMessage());
    }

    public IEnumerator DisplayMessage()
    {
        Debug.Log("Doing my thang");
        int index = Random.Range(0, userList.Length-1);
        string ayuda = userList[index].username;
        Text lbText = Label.GetComponent<Text>();
        lbText.text = ayuda + " te ayudó";
        Image img = backImg.GetComponent<Image>(); 
        backImg.SetActive(true);
        Label.SetActive(true);
        Color Og = lbText.color;
        Color Oi = img.color;
        Button btnAyuda = BA.GetComponent<Button>();
        btnAyuda.interactable = false;

        for (float t = 0.01f; t < 16; t += Time.deltaTime)
        {
            lbText.color = Color.Lerp(Og, Color.clear, Mathf.Min(1, t / 4));
            img.color = Color.Lerp(Oi, Color.clear, Mathf.Min(1, t / 4));
            yield return null;
        }
        BA.SetActive(false);

    }
}
