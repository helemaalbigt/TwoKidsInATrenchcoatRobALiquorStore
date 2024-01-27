using DitzelGames.FastIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsRigManager : MonoBehaviour
{

    public Transform TopKidHandTarget_R, TopKidHandTarget_L, BottomKidHandTarget_R, BottomKidHandTarget_L;
    public FastIKFabric TopKidHandIK_R, TopKidHandIK_L, BottomKidHandIK_R, BottomKidHandIK_L;

	public Vector3 HandOffset_R, HandOffset_L, HandRotOffset_R, HandRotOffset_L;
	// Start is called before the first frame update
	void Start()
	{
		SetupHand(TopKidHandTarget_R,TopKidHandIK_R, HandOffset_R, HandRotOffset_R);
		SetupHand(TopKidHandTarget_L,TopKidHandIK_L, HandOffset_L, HandRotOffset_L);

		BottomKidHandIK_L.Target = BottomKidHandTarget_L;
		BottomKidHandIK_R.Target = BottomKidHandTarget_R;
	}

	private void SetupHand(Transform TopKidHandTarget, FastIKFabric TopKidHandIK, Vector3 HandOffset, Vector3 HandRotOffset)
	{
		Transform TopKidHandTarget_R_Offsetted = new GameObject().transform;
		TopKidHandTarget_R_Offsetted.SetParent(TopKidHandTarget);
		TopKidHandTarget_R_Offsetted.localPosition = HandOffset;
		TopKidHandTarget_R_Offsetted.localRotation = Quaternion.Euler( HandRotOffset);

		TopKidHandIK.Target = TopKidHandTarget_R_Offsetted;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
