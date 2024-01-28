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

    public Transform headTransform;

    public Transform[] pointsOfInterest;
    int pointsOfInterestIndex;

    public UnityEvent OnAlerted;
    public UnityEvent OnAlertedEnded;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip ShopkeeperAlerted, ShopkeeperNoticedStealing;

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
            newRotation = Quaternion.LookRotation(pointOfInterest.position - headTransform.position);
            //newRotation = Quaternion.Euler(0, Random.Range(-randomRotationRange, randomRotationRange), 0);
        }

        headTransform.rotation = Quaternion.Lerp(headTransform.rotation, newRotation, .05f);

    }

    private void LookAtBottleInAllertZone()
    {
        Transform BottleInAllertZone = GameManager.Instance.BottleInAllertZone;
        Transform BottleInCoatZone = GameManager.Instance.BottleInCoatZone;
        if (BottleInAllertZone != null && BottleInCoatZone != null)
        {
            audioSource.PlayOneShot(ShopkeeperNoticedStealing);
            GameManager.Instance.PlayerLost = true;
        }

        if (BottleInAllertZone == null)
		{
			EndAlertState();
			return;
        }

        if (Time.unscaledTime - alertedTimerStart > AlertedDuration)
        {
            EndAlertState();
        }

        newRotation = Quaternion.LookRotation(BottleInAllertZone.position - headTransform.position);
        headTransform.rotation = Quaternion.Lerp(headTransform.rotation, newRotation, .05f);
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

        Vector3 directionToTarget = BottleInAllertZone.position - headTransform.position;
        float angleToTarget = Vector3.Angle(headTransform.forward, directionToTarget);

        if (directionToTarget.magnitude <= detectionDistance && angleToTarget <= fieldOfView)
        {
            RaycastHit hit;
            // Cast a ray from the shopkeeper to the target
            if (Physics.Raycast(headTransform.position, directionToTarget.normalized, out hit, detectionDistance))
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