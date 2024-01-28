using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool StartScreenActive;
    public bool GameActive;
    public bool EndScreenActive;

    public bool PlayerLost;

    public Transform BottleInAllertZone;
    public Transform BottleInCoatZone;


    public UnityEvent OnPlayerLost;
    public UnityEvent OnPlayerWon;


    public AudioSource BellAudioSource, TopKidAudioSource, BottomKidAudioSource;
    public AudioClip BellSound, KidsInstructions;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        StartScreenActive = true;

        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (StaticGameData.stolenBottles == null) {
            StaticGameData.stolenBottles = new List<(string, float)>();
        } else {
            StaticGameData.stolenBottles.Clear();
        }

        StolenBottleManager.I.ClearBottleList();

        StartCoroutine(IntroLoop());
    }

    IEnumerator IntroLoop()
	{
        yield return null;

        yield return new WaitForSeconds(5);
        BellAudioSource.PlayOneShot(BellSound);
        
        while(BellAudioSource.isPlaying)
            yield return null;

        yield return new WaitForSeconds(2);
        BottomKidAudioSource.PlayOneShot(KidsInstructions);

        while (BellAudioSource.isPlaying)
            yield return null;


        // more dialog?


        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {

        yield return null;

        while (true)
        {
            Debug.Log("GameLoop");
            if (PlayerLost)
            {
                OnPlayerLost.Invoke();
                StartCoroutine(EndGame(true));
                break;
            }

			if (StolenBottleManager.I.GetBottleList().Count >= 4)
			{
				OnPlayerWon.Invoke();
				StartCoroutine(EndGame(false));
				break;
			}
			yield return null;
        }
    }

    IEnumerator EndGame(bool lose)
    {
        //Debug.LogError(lose + " " + StolenBottleManager.I.GetBottleList().Count);
        yield return new WaitForSeconds(2);

        StaticGameData.gotCaught = lose;
        StaticGameData.stolenBottles = new List<(string, float)>();
        foreach (var bottle in StolenBottleManager.I.GetBottleList())
        {
            StaticGameData.stolenBottles.Add((bottle.bottleName, bottle.price));
        }

        SceneManager.LoadScene(2);
    }


}
