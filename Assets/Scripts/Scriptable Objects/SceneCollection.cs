using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "new Scene Collection", menuName = "Scriptable Objects/Scene Collection")]
public class SceneCollection : ScriptableObject
{
    public SceneAsset mainMenu;
    public List<SceneAsset> levels;

    public SceneAsset GetLevel(int index)
    {
        return index >= 0 && index < levels.Count ? levels[index] : null;
    }
}