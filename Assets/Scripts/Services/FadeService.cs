using System;
using System.Collections;
using UnityEngine;
using WeappyTest;

[ZenjectBindingInstanceAsSingle]
public class FadeService : MonoBehaviour
{
    [SerializeField]
    private Material _fadeMaterial;
    [SerializeField]
    private float _fadeMax = 1;
    [SerializeField]
    private float _fadeAnimationTime = 1;

    private float _fade = 0;
    private float Fade
    {
        get => _fade;
        set
        {
            _fade = value;
            _fadeMaterial.SetFloat(FadePropName, _fade);
        }
    }

    private Coroutine _fadeCoroutine;
    private static readonly int FadePropName = Shader.PropertyToID("_Fade");

    private void Awake()
    {
        Fade = _fadeMax;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _fadeMaterial);
    }

    public void FadeIn(Action callback)
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeTo(0, _fadeAnimationTime, callback));
    }

    public void FadeOut(Action callback)
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeTo(_fadeMax, _fadeAnimationTime, callback));
    }

    private IEnumerator FadeTo(float newFadeValue, float fadeTime, Action callback)
    {
        var oldFadeValue = Fade;
        var startTime = Time.realtimeSinceStartup;
        var timeElapsed = 0f;
        while (timeElapsed < fadeTime)
        {
            Fade = Mathf.Lerp(oldFadeValue, newFadeValue, timeElapsed / fadeTime);
            timeElapsed = Time.realtimeSinceStartup - startTime;
            yield return new WaitForSeconds(fadeTime / 3.1f);
        }
        Fade = newFadeValue;
        callback?.Invoke();
    }
}
