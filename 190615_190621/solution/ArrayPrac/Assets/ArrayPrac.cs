using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayPrac : MonoBehaviour {
    public int[] NumberArray = new int[10];

    public Transform ArrayItemParent;
    public GameObject ArrayPanel;

    public void SetArrayValue(int index, int value) {
        NumberArray[index] = value;
    }

    public void Sort() {
        //SwapSort();
        //BubbleSort();
        SelectionSort();

        FlashUI();
    }

    private void SwapSort() {
        for (int i = 0; i < NumberArray.Length - 1; i++) {
            for (int j = i + 1; j < NumberArray.Length; j++) {
                if (NumberArray[i] > NumberArray[j]) {
                    int temp = NumberArray[i];
                    NumberArray[i] = NumberArray[j];
                    NumberArray[j] = temp;
                }
            }
        }
    }
    private void BubbleSort() {
        for (int i = NumberArray.Length - 1; i > 0; i--) {
            for (int j = 0; j < i; j++) {
                if (NumberArray[j] > NumberArray[j + 1]) {
                    int temp = NumberArray[j];
                    NumberArray[j] = NumberArray[j + 1];
                    NumberArray[j + 1] = temp;
                }
            }
        }
    }
    private void SelectionSort() {
        for (int i = 0; i < NumberArray.Length - 1; i++) {
            int index = i;
            for (int j = i + 1; j < NumberArray.Length; j++) {
                if (NumberArray[j] < NumberArray[index]) {
                    index = j;
                }
            }
            int temp = NumberArray[i];
            NumberArray[i] = NumberArray[index];
            NumberArray[index] = temp;
        }
    }

    private void FlashUI() {
        foreach (Transform child in ArrayItemParent) {
            child.GetComponentInChildren<InputField>().text =
                NumberArray[int.Parse(child.name.Substring(child.name.IndexOf("_") + 1))].ToString();
        }
    }

    // Use this for initialization
    void Start () {
        for(int i = 0; i < NumberArray.Length; ++i) {
            GameObject g = Instantiate(ArrayPanel, ArrayItemParent);
            g.transform.name += "_" + i;

            g.GetComponentInChildren<Text>().text = "数组[" + i + "]";
            g.GetComponentInChildren<InputField>().text = NumberArray[i].ToString();

            Button button = g.GetComponentInChildren<Button>();
            button.onClick.AddListener(
                delegate() {
                    this.SetArrayValue(int.Parse(g.transform.name.Substring(g.transform.name.IndexOf("_") + 1)), 
                        int.Parse(g.GetComponentInChildren<InputField>().text));
                }
            );
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
