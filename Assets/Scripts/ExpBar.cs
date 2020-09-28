using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode()]
public class ExpBar : MonoBehaviour
{

    //TODO: on click acelerar animacion

    public int max;
    public int min;
    public int previousCurrent;
    public int current;
    public Image bar;
    public Transform particles;
    public Text expValues;
    bool shouldAnimate = false;
    int animationRate;
    int skippedFrames = 0;
    double weight;
    RectTransform edge;


    // Start is called before the first frame update
    void Start()
    {
        edge = particles.GetComponent<RectTransform>();
        animationRate = (max - min) - (current - previousCurrent);
        weight = (current - max) / 2;
        getCurrentFill();
        StartCoroutine(WaitBeforeExpGain());
        expValues.text = "Exp: " + previousCurrent.ToString() + "/" + max.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldAnimate) {
            if(previousCurrent <= current) { 
                animateFill();
            }
            else {
                shouldAnimate = false;
                particles.GetComponent<ParticleSystem>().Stop();
                Database.setExpBarData(max, min, current);
                Database.saveData();
                WinCase.enableButtons();
            }
        }
        
    }

    IEnumerator WaitBeforeExpGain() {
        if (current != previousCurrent) {
            yield return new WaitForSeconds(2);
            shouldAnimate = true;
        }
        else {
            WinCase.enableButtons();
        }
        
    }

    void getCurrentFill() {
        float currentOffset = previousCurrent - min;
        float maximumOffset = max - min;
        float fillAmount = currentOffset / maximumOffset;

        bar.fillAmount = fillAmount;
    }

    void animateFill() {

        //Skips certain amount of frames to make animation more smooth
        if (skippedFrames < -1 * animationRate / weight) {
            skippedFrames++;
            return;
        }

        if (previousCurrent == max) {
            //Aqui se ajusta la growth rate de la exp necesaria para subir de nivel
            max += max + max / 2;
            min = previousCurrent;
            animationRate = (max - min) - (current - previousCurrent);
            weight *= 2;
        }

        float currentOffset = previousCurrent - min;
        float maximumOffset = max - min;
        float fillAmount = currentOffset / maximumOffset;

        bar.fillAmount = fillAmount;
        particles.gameObject.SetActive(true);
        expValues.text = "Exp: " + previousCurrent.ToString() + "/" + max.ToString();
        previousCurrent += 1;

        edge.anchorMin = new Vector2(fillAmount, edge.anchorMin.y);
        edge.anchorMax = new Vector2(fillAmount, edge.anchorMin.y);
        edge.anchoredPosition = Vector2.zero;

        if (animationRate < 0) {
            skippedFrames = 0;
        }

    }

}
