using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSphereAnimator : MonoBehaviour
{
    public float duration = 0.3f;
    public float targetRadius = 3f;
    public float startAlpha = 0.75f;

    private float timer = 0f;
    private Material mat;
    private Color baseColor;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;
        baseColor.a = startAlpha;
        mat.color = baseColor;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float fraction = timer / duration;

        float scale = Mathf.Lerp(0f, targetRadius * 2f, fraction);
        transform.localScale = new Vector3(scale, scale, scale);

        Color c = baseColor;
        c.a = Mathf.Lerp(startAlpha, 0f, fraction);
        mat.color = c;

        if (fraction >= 1f)
            Destroy(gameObject);
    }
}
