using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroScreenManager : MonoBehaviour {
    public GameObject failWrapper;
    public GameObject winWrapper;

    public Text winText;
    public GameObject winContinueWrapper;
    
    public StolenItem prefab;
    public RectTransform parent;
    
    public bool debug;
    
    void Start() {
        StartCoroutine(ShowEndScreen());
    }

    IEnumerator ShowEndScreen() {
        if (debug) {
            StaticGameData.gotCaught = false;
            StaticGameData.stolenBottles = new List<(string, float)>();
            StaticGameData.stolenBottles.Add(("Fancy Champagne", 9.99f));
            StaticGameData.stolenBottles.Add(("Herresy Cognac", 14.99f));
            StaticGameData.stolenBottles.Add(("Jack Damiel", 19.99f));
            StaticGameData.stolenBottles.Add(("Stellar Beer", 49.99f));
        }

        if (StaticGameData.gotCaught) {
            failWrapper.SetActive(true);
            winWrapper.SetActive(false);

            while (!Input.GetMouseButtonDown(0)) {
                yield return null;
            }
            
            SceneManager.LoadScene(0);
        } else {
            failWrapper.SetActive(false);
            winWrapper.SetActive(true);
            winContinueWrapper.SetActive(false);

            var total = 0f;
            foreach (var bottle in StaticGameData.stolenBottles) {
                total += bottle.Item2;
            }

            winText.text = $"You got away with ${total.ToString("F2")} worth of alcohol!";

            foreach (var bottle in StaticGameData.stolenBottles) {
                yield return new WaitForSeconds(1f);
                var item = Instantiate(prefab, parent);
                item.Show(bottle.Item1, bottle.Item2);
            }
            yield return new WaitForSeconds(1f);
            
            winContinueWrapper.SetActive(true);
            while (!Input.GetMouseButtonDown(0)) {
                yield return null;
            }
            
            SceneManager.LoadScene(0);
        }
    }
}
