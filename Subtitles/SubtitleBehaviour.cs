using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
public class SubtitleBehaviour : PlayableBehaviour
{
    public string subtitleText;
    // Start is called before the first frame update
/*    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        text.text = subtitleText;
        text.color = new Color(1, 1, 1, info.weight);
    }*/
}
