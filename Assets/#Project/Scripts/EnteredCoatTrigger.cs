using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredCoatTrigger : MonoBehaviour
{
    List<GameObject> ObjectsInTrigger = new List<GameObject>();
	private GameManager gameManager;

	private void Awake()
	{

        gameManager = FindObjectOfType<GameManager>();
    }
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OVRGrabbable>() != null)
        {
            ObjectsInTrigger.Add(other.gameObject);
            gameManager.BottleInCoatZone = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<OVRGrabbable>() != null)
        {
            if (ObjectsInTrigger.Contains(other.gameObject))
                ObjectsInTrigger.Remove(other.gameObject);
            if (ObjectsInTrigger.Count == 0)
            {
                gameManager.BottleInCoatZone = null;
            }
        }
    }

}
