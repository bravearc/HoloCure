using UnityEngine;
using UnityEngine.U2D;
using System;

public class AtlasLoader : MonoBehaviour
{
    private void Awake()
    {
        SpriteAtlasManager.atlasRequested += RequestAtlas;
    }

    private void OnDestroy()
    {
        SpriteAtlasManager.atlasRequested -= RequestAtlas;
    }

    private void RequestAtlas(string tag, Action<SpriteAtlas> action)
    {
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>("Atlas/" + tag);
        if (atlas != null)
        {
            action(atlas);
        }
        else
        {
            Debug.LogError("Sprite Atlas not found: " + tag);
        }
    }
}
