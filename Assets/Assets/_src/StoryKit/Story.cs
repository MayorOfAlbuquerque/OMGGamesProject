using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "A Story", menuName = "MurderMystery/Story", order = 5)]
public class Story : ScriptableObject {
    [Tooltip("Unique ID given to each story.")]
    public int StoryId = -1;

    [Tooltip("Unique story name.")]
    public string Name;

    public CharacterList PlayableCharacters;

    public CharacterSpec Murderer;

    [Tooltip("Scenes for the story. Loaded at runtime in the order.")]
    public List<SceneReference> scenes;
}
