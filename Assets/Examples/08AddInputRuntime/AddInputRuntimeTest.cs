using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class AddInputRuntimeTest : MonoBehaviour
{
    public AnimationClip clip0;
    public AnimationClip clip1;
    public float weight;

    PlayableGraph playableGraph;
    AnimationMixerPlayable mixerPlayable;

    private void Start()
    {
        playableGraph = PlayableGraph.Create();

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation Mixer", GetComponent<Animator>());

        // 这里不指定初始大小
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph);
        playableOutput.SetSourcePlayable(mixerPlayable);

        var clipPlayable0 = AnimationClipPlayable.Create(playableGraph, clip0);
        var clipPlayable1 = AnimationClipPlayable.Create(playableGraph, clip1);

        // 通过 AddInput 来添加 playable
        mixerPlayable.AddInput(clipPlayable0, 0, 0f);
        mixerPlayable.AddInput(clipPlayable1, 0, 0f);

        playableGraph.Play();
    }

    private void Update()
    {
        weight = Mathf.Clamp01(weight);
        mixerPlayable.SetInputWeight(0, 1.0f - weight);
        mixerPlayable.SetInputWeight(1, weight);
    }

    private void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
