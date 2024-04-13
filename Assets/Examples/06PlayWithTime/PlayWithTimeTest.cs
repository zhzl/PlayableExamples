using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayWithTimeTest : MonoBehaviour
{
    public AnimationClip clip;
    public float time;

    PlayableGraph playableGraph;
    AnimationClipPlayable clipPlayable;

    private void Start()
    {
        playableGraph = PlayableGraph.Create();

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Play Animation Clip", GetComponent<Animator>());

        clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        playableOutput.SetSourcePlayable(clipPlayable);

        playableGraph.Play();

        clipPlayable.Pause();
    }

    private void Update()
    {
        clipPlayable.SetTime(time);
    }

    private void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
