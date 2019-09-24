using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdginessGrowth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject polyObject;

    private float minScale = 0.3f;
    private float maxScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polyObject = transform.parent.GetComponentInChildren<PolyShooter>().gameObject;
    }

    public void updatePolyObject()
    {
        polyObject = transform.parent.GetComponentInChildren<PolyShooter>().gameObject;
        spriteRenderer.sprite = polyObject.GetComponent<SpriteRenderer>().sprite;
        updateTransform();
        this.updateScale(0);
    }

    public void updateScale(float scale)
    {
        float interpolatedScale = (maxScale - minScale) * scale + minScale;
        this.transform.localScale = new Vector3(interpolatedScale, interpolatedScale, interpolatedScale);
    }

    public void updateTransform()
    {
        if (polyObject == null) return;
        this.transform.localPosition = polyObject.transform.localPosition;
        this.transform.rotation = polyObject.transform.rotation;
    }
}
