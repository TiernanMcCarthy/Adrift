using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraMode
{
    [TextArea]
    public string modeName;

    public Transform parent;


    public float FOV;

    public Vector3 position;

    public Vector3 rotation;

    public bool resetPos;


    public void SetCameraMode(CameraManager cam)
    {
        if (parent != null)
        {
            cam.transform.parent = parent;
        }
        else
        {
            cam.transform.parent = null;
        }

        cam.cam.fieldOfView = FOV;

        if (resetPos)
        {
            cam.transform.position = position;

            cam.transform.localRotation = Quaternion.Euler(Vector3.zero);

        }

    }
}

public class CameraManager : MonoBehaviour
{

    public List<CameraMode> modes = new List<CameraMode>();

    public Camera cam;

    public GameObject connexionManager;

    public Transform raycastPoint;

    public Transform grabPosition;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ChangeCameraMode(string modeName)
    {
        foreach (CameraMode mode in modes)
        {
            if (mode.modeName == modeName)
            {
                {
                    mode.SetCameraMode(this);
                }
            }
        }
    }
    bool stopUI = false;
    /*
    protected virtual void OnGUI()
    {
        if (!stopUI)
        {
            GUILayout.BeginArea(new Rect(100, 100, Screen.width - 10, Screen.height - 10));
            {
                GUILayout.BeginVertical(GUI.skin.window);
                {

                    if (GUILayout.Button("Start Game"))
                    {
                        connexionManager.gameObject.SetActive(true);
                        var runner = connexionManager.GetComponent<NetworkRunner>();
                        if (runner)
                        {
                            // As the runner was disabled, the runner may not have auto registered its listeners
                            foreach (var listener in runner.GetComponentsInChildren<INetworkRunnerCallbacks>())
                            {
                                runner.AddCallbacks(listener);
                            }
                        }
                        stopUI = true;
                        //ChangeCameraMode("Gameplay");
                        //EnableVRRig();
                    }

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }*/

}
