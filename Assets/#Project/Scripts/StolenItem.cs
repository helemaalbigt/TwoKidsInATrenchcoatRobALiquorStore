using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StolenItem : MonoBehaviour {
    public NameObjectPair[] pairs;
    public Text name;
    public Text price;

    public void Show(string name, float price) {
        foreach (var p in pairs) {
            if (p.name == name) {
                p.obj.SetActive(true);
            } else {
                p.obj.SetActive(false);
            }
        }

        this.name.text = name;
        this.price.text = "$" + price.ToString("F2");
    }
}

[System.Serializable]
public class NameObjectPair {
    public string name;
    public GameObject obj;
}
