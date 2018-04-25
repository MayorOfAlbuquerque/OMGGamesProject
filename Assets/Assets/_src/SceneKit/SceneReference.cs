using System;
using UnityEngine;
using System.Collections;

[Serializable]
[CreateAssetMenu(fileName = "New Scene Reference", menuName = "MurderMystery/Scene Part Reference", order = 6)]
public class SceneReference : ScriptableObject
{
    public string sceneName = String.Empty;
    public string scenePath = String.Empty;
}
