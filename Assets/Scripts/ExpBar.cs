using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ExpBar : MonoBehaviour
{

    public int max;
    public int min;
    public int current;
    public Image bar;
    public Transform particles;
    public Text expValues;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getCurrentFill();
    }

    void getCurrentFill() {

        RectTransform edge = particles.GetComponent<RectTransform>();

        float currentOffset = current - min;
        float maximumOffset = max - min;
        float fillAmount = currentOffset / maximumOffset;
        bar.fillAmount = fillAmount;

        expValues.text = "Exp: " + current.ToString() + "/" + max.ToString();

        edge.anchorMin = new Vector2(fillAmount, edge.anchorMin.y);
        edge.anchorMax = new Vector2(fillAmount, edge.anchorMin.y);
        edge.anchoredPosition = Vector2.zero;
    }

}
