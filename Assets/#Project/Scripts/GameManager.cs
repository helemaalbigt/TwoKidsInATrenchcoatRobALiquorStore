using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            if (PlayerLost)
            {
                OnPlayerLost.Invoke();
                StartCoroutine(EndGame(false));
                break;
            }

            //if (StaticGameData.stolenBottles.Count >= 4)
            //{
            //    OnPlayerWon.Invoke();
            //    StartCoroutine(EndGame(true));
            //    break;
            //}
            yield return null;
        }
    }

    IEnumerator EndGame(bool lose)
    {

        yield return new WaitForSeconds(2);

        StaticGameData.gotCaught = lose;
        StaticGameData.stolenBottles = StolenBottleManager.I.GetBottleList();

        Debug.Log(" END GAME STATE : " + StaticGameData.gotCaught);
	}


}
