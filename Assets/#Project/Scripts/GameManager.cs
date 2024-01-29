using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool PlayerLost;
    public bool ShopKeeperAlerted;

    public Transform BottleInAllertZone;
    public Transform BottleInCoatZone;


    public UnityEvent OnPlayerLost;
    public UnityEvent OnPlayerWon;
    public UnityEvent OnPlayerGrabbedBottle;


    public AudioSource BellAudioSource, TopKidAudioSource, BottomKidAudioSource, ShopKeeperAudioSource;
    public AudioClip BellSound, KidsInstructions, ShopKeeperIntro;

    private void Awake()
    {

        if (StaticGameData.stolenBottles == null) {
            StaticGameData.stolenBottles = new List<(string, float)>();
        } else {
            StaticGameData.stolenBottles.Clear();
        }

        StolenBottleManager.I.ClearBottleList();
        PlayerLost = false;
        Debug.Log("AWAKE");
        StartCoroutine(IntroLoop());
    }

    public void PlayerGrabbedBottle()
	{
        OnPlayerGrabbedBottle.Invoke();
    }
    
    public void OnBottomPlayerGrabbedBottle()
	{
        if (ShopKeeperAlerted)
            PlayerLost = true;
    }

    IEnumerator IntroLoop()
	{
        yield return null;

        yield return new WaitForSeconds(2);
        BellAudioSource.PlayOneShot(BellSound);
        
        while(BellAudioSource.isPlaying)
            yield return null;

        yield return new WaitForSeconds(2);
        ShopKeeperAudioSource.PlayOneShot(ShopKeeperIntro);
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

			if (Input.GetKeyDown(KeyCode.Space))
			{
                StartCoroutine(EndGame(false));
            }
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
