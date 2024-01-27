using DitzelGames.FastIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsRigManager : MonoBehaviour
{

    public Transform TopKidHandTarget_R, TopKidHandTarget_L, BottomKidHandTarget_R, BottomKidHandTarget_L;
    public FastIKFabric TopKidHandIK_R, TopKidHandIK_L, BottomKidHandIK_R, BottomKidHandIK_L;

	public Vector3 HandOffset_R, HandOffset_L, HandRotOffset_R, HandRotOffset_L;

	public Transform BottomKidHeadBone, TopKidHeadBone;


	private Animator animator;
	private BottomHandController bottomHandController;
	private AutoHeadRecenter autoHeadRecenter;

	// Start is called before the first frame update
	void Start()
	{

		SetupHand(TopKidHandTarget_R,TopKidHandIK_R, HandOffset_R, HandRotOffset_R);
		SetupHand(TopKidHandTarget_L,TopKidHandIK_L, HandOffset_L, HandRotOffset_L);

		BottomKidHandIK_L.Target = BottomKidHandTarget_L;
		BottomKidHandIK_R.Target = BottomKidHandTarget_R;

		animator = GetComponent<Animator>();

		bottomHandController = FindObjectOfType<BottomHandController>();
		autoHeadRecenter = FindObjectOfType<AutoHeadRecenter>();
	}

	private void Update()
	{
		BottomKidHeadBone.rotation = bottomHandController.camera.transform.rotation;
		TopKidHeadBone.rotation = autoHeadRecenter.head.rotation;
	}

	private void SetupHand(Transform TopKidHandTarget, FastIKFabric TopKidHandIK, Vector3 HandOffset, Vector3 HandRotOffset)
	{
		Transform TopKidHandTarget_R_Offsetted = new GameObject().transform;
		TopKidHandTarget_R_Offsetted.SetParent(TopKidHandTarget);
		TopKidHandTarget_R_Offsetted.localPosition = HandOffset;
		TopKidHandTarget_R_Offsetted.localRotation = Quaternion.Euler( HandRotOffset);

		TopKidHandIK.Target = TopKidHandTarget_R_Offsetted;
	}


	public void SetTopKidHandPose_R(float value)
	{
		animator.SetLayerWeight(2, value);
	}
	public void SetTopKidHandPose_L(float value)
	{
		animator.SetLayerWeight(1, value);
	}
	public void SetBottomKidHandPose_R(float value)
	{
		animator.SetLayerWeight(3, value);
	}
}
