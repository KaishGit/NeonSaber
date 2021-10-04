using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialog;
    public SentendInfo[] sentences;

    [Serializable]
    public struct SentendInfo
    {
        [TextArea]
        public string text;
        public float delayTime;
        public float delayToNext;
        public bool eraseToNext;
    }

    private float currentTime = 0f;
    private float currentTimeToNext = 0f;

    private char[] charArray;
    private int indexText = 0;
    private int indexSentence;

    SentendInfo currentSentence;

    // Start is called before the first frame update
    void Start()
    {
        currentSentence = sentences[indexSentence];
        charArray = currentSentence.text.ToCharArray();
        dialog.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (indexText < charArray.Length && currentTime > currentSentence.delayTime)
        {
            currentTime = 0f;

            dialog.text += charArray[indexText];

            indexText++;
        }
        else if (indexText >= charArray.Length)
        {
            currentTimeToNext += Time.deltaTime;
        }

        if (indexSentence < sentences.Length - 1 && currentTimeToNext > currentSentence.delayToNext)
        {
            if (currentSentence.eraseToNext)
            {
                dialog.text = "";
            }
            currentTimeToNext = 0f;
            indexSentence++;
            indexText = 0;
            currentSentence = sentences[indexSentence];
            charArray = currentSentence.text.ToCharArray();
        }
    }
}
