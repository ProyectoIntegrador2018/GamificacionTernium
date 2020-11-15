using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBehavior : MonoBehaviour
{
    public int id;
    //public GameObject titleObject;
    //public GameObject descObject;
    private static News news;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void borrar(){
        Debug.Log(id);
        news = ToastManager.getNews();
        news.Delete(id);
        ToastManager.updateNews(news);
        Destroy(transform.parent.gameObject);
    }

    /*public void editar(){
        titleObject.SetActive(true);  
    }*/

    public void setID(int assigned){
        id = assigned;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
