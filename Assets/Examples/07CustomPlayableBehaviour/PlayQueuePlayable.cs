using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayQueuePlayable : PlayableBehaviour
{
    private int m_CurrentClipIndex = -1;
    private float m_TimeToNextClip;
    private Playable mixter;

    public void Initialize(AnimationClip[] clipsToPlay, Playable owner, PlayableGraph graph)
    {
        owner.SetInputCount(1);

        mixter = AnimationMixerPlayable.Create(graph, clipsToPlay.Length);
        graph.Connect(mixter, 0, owner, 0);
        owner.SetInputWeight(0, 1f);

        for (int clipIndex = 0; clipIndex < mixter.GetInputCount(); clipIndex++)
        {
            graph.Connect(AnimationClipPlayable.Create(graph, clipsToPlay[clipIndex]), 0, mixter, clipIndex);
            mixter.SetInputWeight(clipIndex, 1.0f);
        }
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (mixter.GetInputCount() == 0)
            return;

        m_TimeToNextClip -= info.deltaTime;
        if (m_TimeToNextClip <= 0f)
        {
            m_CurrentClipIndex++;

            if (m_CurrentClipIndex >= mixter.GetInputCount())
            {
                m_CurrentClipIndex = 0;
            }

            var currentClip = (AnimationClipPlayable)mixter.GetInput(m_CurrentClipIndex);
            currentClip.SetTime(0);

            m_TimeToNextClip = currentClip.GetAnimationClip().length;
        }

        // 调整权重
        for (int clipIndex = 0; clipIndex < mixter.GetInputCount(); clipIndex++)
        {
            if (clipIndex == m_CurrentClipIndex)
            {
                mixter.SetInputWeight(clipIndex, 1f);
            }
            else
            {
                mixter.SetInputWeight(clipIndex, 0f);
            }
        }
    }
}
