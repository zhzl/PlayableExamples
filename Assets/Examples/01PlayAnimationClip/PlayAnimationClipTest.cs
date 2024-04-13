using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayAnimationClipTest : MonoBehaviour
{
    public AnimationClip clip;

    PlayableGraph playableGraph;

    private void Start()
    {
        // AnimationPlayableUtilities ¼ò»¯¶¯»­²¥·Å
        // AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out playableGraph);

        playableGraph = PlayableGraph.Create();

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Play Animation Clip", GetComponent<Animator>());
        
        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        playableOutput.SetSourcePlayable(clipPlayable);

        playableGraph.Play();
    }

    private void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
