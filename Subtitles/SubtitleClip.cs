using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class SubtitleClip : PlayableAsset
{
    private SubtitleBehaviour subtitleBehavior;
    public string subtitleText;

    // Start is called before the first frame update
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);
        subtitleBehavior = playable.GetBehaviour();
        subtitleBehavior.subtitleText = subtitleText;
        return playable;
    }
}
