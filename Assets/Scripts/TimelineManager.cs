using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector playableDirector;

    private void OnEnable()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }
    public void PausePlayback()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void ResumePlayback()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
