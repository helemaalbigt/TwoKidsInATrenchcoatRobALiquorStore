using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAlertZoneTrigger : MonoBehaviour
{

    List<GameObject> ObjectsInTrigger = new List<GameObject>();

	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.GetComponent<OVRGrabbable>() != null)
		{
            ObjectsInTrigger.Add(other.gameObject);
            GameManager.Instance.BottleInAllertZone = other.transform;
        }
    }

	private void OnTriggerExit(Collider other)
	{
        if(other.gameObject.GetComponent<OVRGrabbable>() != null)
		{
            if(ObjectsInTrigger.Contains(other.gameObject))
                ObjectsInTrigger.Remove(other.gameObject);
            if(ObjectsInTrigger.Count == 0)
			{
                GameManager.Instance.BottleInAllertZone = null;
            }
        }
    }

}
