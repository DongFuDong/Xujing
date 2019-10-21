using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimeLine : MonoBehaviour
{

    public PlayableDirector _btn;

    public void OnClickButton()
    {
        _btn.Play();
    }
}
