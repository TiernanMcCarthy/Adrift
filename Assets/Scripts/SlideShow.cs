using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideShow : NetworkBehaviour
{
    [Networked]
    public int slideIndex { get; set; } = 0;

    public int localSlideIndex = 0;

    private int intMaxIndex;

    // Start is called before the first frame update
    void Start()
    {
        intMaxIndex = transform.childCount-1;
    }

    // Update is called once per frame
    void Update()
    {
        if (localSlideIndex != slideIndex)
        {
            if (slideIndex > intMaxIndex)
            {
                SetSlideIndexRPC(0);
            }
            else if (slideIndex < 0)
            {
                SetSlideIndexRPC(intMaxIndex);
            }

            ChangeSlide();
            localSlideIndex = slideIndex;
        }
    }

    void ChangeSlide()
    {
        List<Transform> children = new List<Transform>();
        gameObject.GetComponentsInChildren<Transform>(true,children);

        children.Remove(children[0]);
       // Transform[] children = transform.GetComponentsInChildren<Transform>();

        foreach(Transform t in children)
        {
            t.gameObject.SetActive(false);
        }

        children[slideIndex].gameObject.SetActive(true);


    }

    public override void FixedUpdateNetwork()
    {

    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetSlideIndexRPC(int ind)
    {
        slideIndex = ind;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void ChangeSlideIndexRPC(int ind)
    {
        slideIndex += ind;
    }
}
