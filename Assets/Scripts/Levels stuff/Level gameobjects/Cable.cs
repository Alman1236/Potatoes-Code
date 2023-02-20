using UnityEngine;

public class Cable : MonoBehaviour, IActivable, IResettable
{
    SpriteRenderer mySpriteRenderer;
    Color defaultColor;

    void OnValidate()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
    }

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void IActivable.Activate()
    {
        mySpriteRenderer.color = Color.green;
    }

    void IActivable.Deactivate()
    {
        mySpriteRenderer.color = defaultColor;
    }

    void IResettable.Reset()
    {
        mySpriteRenderer.color = defaultColor;
    }

}
