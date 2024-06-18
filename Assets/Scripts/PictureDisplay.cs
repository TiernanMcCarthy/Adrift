using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureDisplay : MonoBehaviour
{
    public static PictureDisplay instance;

    public Camera viewCamera;

    public Image viewScreen;

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null && viewCamera!=null)
        {
            viewCamera.transform.position=target.position;
            viewCamera.transform.forward = target.forward;
        }
    }

    public void SetTarget(PlayerDetails pd)
    {
        target = pd.networkRig.headset.transform;
    }

    public void ShowPIP()
    {
        viewScreen.gameObject.SetActive(true);
    }

    public void HidePIP()
    {
        viewScreen.gameObject.SetActive(false);
    }

    public void TogglePIP()
    {
        viewScreen.gameObject.SetActive(!viewScreen.gameObject.activeSelf);
    }
}
