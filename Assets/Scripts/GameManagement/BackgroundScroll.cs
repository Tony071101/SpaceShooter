using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Tween scrollTween;
    private MeshRenderer meshRenderer;
    private Material material;
    private float scrollSpeed = 1f;
    float endValueOffsetY;
    //private static BackgroundScroll instance;
    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;
    }

    private void Start()
    {
        StartScrolling();
    }

    private void StartScrolling()
    {
        // Calculate the duration based on the scroll speed
        float duration = transform.localScale.y / scrollSpeed;
        endValueOffsetY = 5f;

        // Set up the scrolling animation using DOTween
        scrollTween = DOTween.To(() => material.GetTextureOffset("_MainTex").y, y => SetTextureOffsetY(y), endValueOffsetY, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void SetTextureOffsetY(float y)
    {
        Vector2 offset = material.GetTextureOffset("_MainTex");
        offset.y = y;
        material.SetTextureOffset("_MainTex", offset);
    }

    private void OnDestroy()
    {
        // Stop and destroy the scroll animation when the object is destroyed
        if (scrollTween != null)
        {
            scrollTween.Kill();
            scrollTween = null;
        }
    }
}
