using UnityEngine;
using UnityEngine.UI;

public class LevelPreviewManager : MonoBehaviour
{
    static Image levelPreviewImage;

    private void Awake()
    {
        GenerateReferences();
    }

    private void GenerateReferences()
    {
        levelPreviewImage = gameObject.GetComponent<Image>();
    }

    static public void UpdateLevelPreview(Sprite levelPreview)
    {
        levelPreviewImage.sprite = levelPreview;
    }
}
