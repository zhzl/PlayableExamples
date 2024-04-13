using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class CustomPlayableBehaviourTest : MonoBehaviour
{
    public AnimationClip[] clipsToPlay;

    PlayableGraph playableGraph;

    private void Start()
    {
        playableGraph = PlayableGraph.Create();

        var playQueuePlayable = ScriptPlayable<PlayQueuePlayable>.Create(playableGraph);
        var playQueue = playQueuePlayable.GetBehaviour();
        playQueue.Initialize(clipsToPlay, playQueuePlayable, playableGraph);

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "CustomPlayable", GetComponent<Animator>());

        playableOutput.SetSourcePlayable(playQueuePlayable, 0);

        playableGraph.Play();
    }

    private void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
