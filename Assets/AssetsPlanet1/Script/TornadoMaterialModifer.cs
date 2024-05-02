using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMaterialModifier : MonoBehaviour
{
    public Material material;
    private float fadeDuration = 3f;
    private float fadeTimer = 0f;

    void Start()
    {
        if (material == null)
        {
            Debug.LogError("Material is not assigned!");
            enabled = false;
            return;
        }

        StartCoroutine(FadeOutMaterial());
    }

    IEnumerator FadeOutMaterial()
    {
        Color color = material.color;

        color.a = 1f;

        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            material.color = color;
            yield return null;
        }

        color.a = 0f;
        material.color = color;
    }
}
