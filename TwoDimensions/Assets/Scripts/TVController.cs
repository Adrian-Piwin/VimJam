using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TVController : MonoBehaviour
{
    public float timeBeforeRemove = 0.3f;
    public float letterAddSpeed = 0.01f;
    public float letterRemoveSpeed = 0.01f;

    public TextMeshPro textObject;
    private bool isEmpty = true;

    public void displayText(string txt){
        StartCoroutine(typeLetters(txt));
    }

    private void removeText(){
        StartCoroutine(removeLetters());
    }

    IEnumerator typeLetters(string txt){
        isEmpty = false;
        string displayText = "";
        foreach (char c in txt){
            yield return new WaitForSeconds(letterAddSpeed);
            displayText = displayText + c;
            updateText(displayText);
        }

        yield return new WaitForSeconds(timeBeforeRemove);
        removeText();
    }

    IEnumerator removeLetters(){
        string displayText = textObject.text;
        for (int i = displayText.Length; i > 0; i--){
            yield return new WaitForSeconds(letterRemoveSpeed);
            displayText = displayText.Remove(displayText.Length - 1, 1);
            updateText(displayText);
        }

        isEmpty = true;
    }

    private void updateText(string txt){
        textObject.SetText(txt);
    }

    public bool getIsEmpty(){
        return isEmpty;
    }
}
