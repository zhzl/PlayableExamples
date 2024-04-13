using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class SetPlayableStateTest : MonoBehaviour
{
    public AnimationClip clip0;
    public AnimationClip clip1;

    PlayableGraph playableGraph;
    AnimationMixerPlayable mixerPlayable;

    private void Start()
    {
        playableGraph = PlayableGraph.Create();

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation Mixer", GetComponent<Animator>());

        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(mixerPlayable);

        var clipPlayable0 = AnimationClipPlayable.Create(playableGraph, clip0);
        var clipPlayable1 = AnimationClipPlayable.Create(playableGraph, clip1);

        playableGraph.Connect(clipPlayable0, 0, mixerPlayable, 0);
        playableGraph.Connect(clipPlayable1, 0, mixerPlayable, 1);

        mixerPlayable.SetInputWeight(0, 0.5f);
        mixerPlayable.SetInputWeight(1, 0.5f);

        // ÔÝÍ£ clipPlayable1
        clipPlayable1.Pause();

        playableGraph.Play();
    }

    private void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
