using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public Camera TopKidCam;
    public Camera BottomKidCam;
    bool _switched;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z)     && Input.GetKey(KeyCode.X)     && Input.GetKey(KeyCode.C))
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
		{
            _switched = !_switched;
            if (_switched)
            {
                TopKidCam.depth = 10;
                BottomKidCam.depth = 0;
            }
            else
            {
                TopKidCam.depth = 0;
                BottomKidCam.depth = 10;
            }
        }
    }

}
