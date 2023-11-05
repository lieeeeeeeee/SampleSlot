using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour {
    public TextMeshProUGUI txt;
    public bool isStop = true;
    private int isRun = 0;
    private int updataFreq = 6;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        changeNum();
    }
    
    private void changeNum() {  
        if(isStop) return;
        if(++isRun!=updataFreq) return;
        int num = UnityEngine.Random.Range(1, 9);
        txt.text = num.ToString();
        isRun = 0;
    }
}
