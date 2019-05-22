using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Interactive Video", menuName = "Interactive Video", order = 2)]
public class Scriptable_Interactive_Video : ScriptableObject
{
    public List<VideoClip> video_to_play = new List<VideoClip>();
}
