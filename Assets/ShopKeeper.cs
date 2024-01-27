using UnityEngine;
using UnityEngine.Events;

public class Shopkeeper : MonoBehaviour
{

    public float detectionDistance = 10f; // Maximum distance to detect the target
    public float fieldOfView = 45f; // Field of view angle
    //public LayerMask obstacleLayer; // Layer on which obstacles are placed

    //public float randomRotationRange = 30;
    public float rotationTimeIntervalSeconds = 30 ;
    public float AlertedDuration = 5 ;
    private float timerStart;
    Quaternion newRotation;

    bool isAlerted;
    private float alertedTimerStart;

    public Transform[] pointsOfInterest;
    int pointsOfInterestIndex;

    public UnityEvent OnAlerted;
    public UnityEvent OnAlertedEnded;

	private void Start()
	{
        // start 
        //EndAlertState();
    }

	private void Update()
	{
        if (isAlerted)
		{
            LookAtBottleInAllertZone();
        }
        else 
        {
            LookAtPointOfInterest();
            //UpdateRandomRotation();
        }

		DetectTarget();

    }
	private void LookAtPointOfInterest()
    {

        if (Time.unscaledTime - timerStart > rotationTimeIntervalSeconds)
        {
            timerStart = Time.unscaledTime;

            Transform pointOfInterest = pointsOfInterest[pointsOfInterestIndex];
            pointsOfInterestIndex++;
            if (pointsOfInterestIndex == pointsOfInterest.Length)
                pointsOfInterestIndex = 0;
            newRotation = Quaternion.LookRotation(pointOfInterest.position - transform.position);
            //newRotation = Quaternion.Euler(0, Random.Range(-randomRotationRange, randomRotationRange), 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, .05f);

    }

    private void LookAtBottleInAllertZone()
    {
        Transform BottleInAllertZone = GameManager.Instance.BottleInAllertZone;
        if (BottleInAllertZone == null)
		{
			EndAlertState();
			return;
        }

        if (Time.unscaledTime - alertedTimerStart > AlertedDuration)
        {
            EndAlertState();
        }

        newRotation = Quaternion.LookRotation(BottleInAllertZone.position - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, .05f);
	}

	private void EndAlertState()
	{
        isAlerted = false;
		OnAlertedEnded.Invoke();
	}

	void DetectTarget()
    {
        Transform BottleInAllertZone =  GameManager.Instance.BottleInAllertZone;
        if (BottleInAllertZone == null) return;

        Vector3 directionToTarget = BottleInAllertZone.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        if (directionToTarget.magnitude <= detectionDistance && angleToTarget <= fieldOfView)
        {
            RaycastHit hit;
            // Cast a ray from the shopkeeper to the target
            if (Physics.Raycast(transform.position, directionToTarget.normalized, out hit, detectionDistance))
            {
                // Check if the first object hit by the raycast is indeed the target
                if (hit.transform == BottleInAllertZone)
				{
					StartAlertedState();
				}
			}
        }
    }

	private void StartAlertedState()
	{
        alertedTimerStart = Time.unscaledTime;
        isAlerted = true;
		OnAlerted.Invoke();
	}

}
