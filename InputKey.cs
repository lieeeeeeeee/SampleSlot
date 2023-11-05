using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputKey : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI hitCountTxt;
    public TextMeshProUGUI rotateCountTxt;
    private bool isReach = false;
    private bool isHit = false;
    private int rotateCount = 0;
    private int reachRate = 5;
    private int hitRate = 10;
    private int hitCount = 0;
    private int slotIndex = 0;
    private int firstSlotIndex = 0;
    private int[] slot = {-1,-1,-1};
    void Start() {
        var usrInput = str();
        var usrSum = sum(1, 2);
    }
    // Update is called once per frame
    void Update() {
        OnInputKey();
    }
    private string str() {
        var str = "ユーザの入力を待つ";
        return str;
    }
    private int sum(int a, int b) {
        return a + b;
    }
 
    private void OnInputKey() {
        if(!Input.anyKeyDown) return;
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("Left Arrow");
            if(slot[0] == 0) action(0, false);
        } else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("Down Arrow");
            if(slot[1] == 0) action(1, false);
        } else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("Right Arrow");
            if(slot[2] == 0) action(2, false);
        } else if(Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("Return");
            isHit = false;
            isReach = false;
            slot = new int[] {0,0,0};
            if(hitRate == new System.Random().Next(1, hitRate+1)) isHit = true;
            if(reachRate == new System.Random().Next(1, reachRate+1)) isReach = true;
            rotateCountTxt.text = "回転数: "+(++rotateCount).ToString()+"回";

            action(0, true);
            action(1, true);
            action(2, true);
        }

        void action(int i, bool isReturen) {
            GameObject num_background = transform.GetChild(i).gameObject;
            GameObject num_text = num_background.transform.GetChild(0).gameObject;
            Slot classSlot = num_text.GetComponent<Slot>();
            Color color = (!isReturen&&(isHit||isReach)) ? Color.red : Color.green;
            int rand = new System.Random().Next(1, 10);

            classSlot.isStop = (isReturen) ? false : true;
            
            if(!isReturen) { 
                
                Debug.Log("isReturen||!isHit||!isReach: "+(isReturen||!isHit||!isReach));
                slot[i] = rand;
                switch (slotIndex) {
                    case 0:
                        firstSlotIndex = i;
                        slotIndex++;
                        break;
                    case 1:
                        if(slot[i] == slot[firstSlotIndex]) slot[i] = Mathf.CeilToInt((float)slot[i]*0.5f);
                        if(isHit||isReach) slot[i] = slot[firstSlotIndex];
                        
                        slotIndex++;
                        break;
                    case 2:
                        if(slot[i] == slot[firstSlotIndex]) slot[i] = Mathf.CeilToInt((float)slot[i]*0.5f);
                        if(slot.Distinct().Count() == 1) isHit = true;

                        if(isHit) {
                            slot[i] = slot[firstSlotIndex];
                            hitCount++;
                        }
                        hitCountTxt.text = "当たり回数: "+hitCount.ToString()+"回";
                        color = (isHit) ? Color.yellow : Color.green;
                        slotIndex = 0;
                        break;
                }
                classSlot.txt.text = slot[i].ToString();
            }
            num_background.GetComponent<Image>().color = color;
        }
    }
}
