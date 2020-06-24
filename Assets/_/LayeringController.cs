using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeringController : MonoBehaviour
{

    public Shader shader;

    [System.NonSerialized]
    public Material material;

    private RenderTexture _lastTex;

    [SerializeField] private float _srcAlpha = 0.1f;

    [SerializeField] private float _colorLerp = 0.5f;

    [SerializeField] private float _colorAlpha = 0.1f;

    private float _isClear = 0.0f;

    [SerializeField] private bool _isPost = true;

    void Start()
    {
        if (this.shader == null)
        {
            throw new Exception("Property \"shader\" must be set");
        }

        this.material = new Material(this.shader);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            this._isClear = 1.0f;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            this._isClear = 2.0f;
        }
    }

    void UpdateLayering()
    {
        if (this._lastTex != null)
        {
            this.material.SetTexture("_LastTex", this._lastTex);
            this.material.SetTexture("_MainTex", this._lastTex);
            this.material.SetFloat("_SrcAlpha", this._srcAlpha);
            this.material.SetFloat("_ColorLerp", this._colorLerp);
            this.material.SetFloat("_ColorAlpha", this._colorAlpha);
            this.material.SetFloat("_IsClear", this._isClear);

            if (this._isClear > 0)
            {
                Debug.Log("isClear");
            }

            this._isClear = 0.0f;
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (_isPost)
        {
            if (this._lastTex == null)
            {
                DestroyImmediate(this._lastTex);
                this._lastTex = null;
                this._lastTex = new RenderTexture(src.width, src.height, 0, src.format);
                this._lastTex.hideFlags = HideFlags.HideAndDontSave;
                Graphics.Blit(src, this._lastTex);
            }


            this.UpdateLayering();
            this._lastTex.MarkRestoreExpected();

            Graphics.Blit(this._lastTex, src, this.material);
            Graphics.Blit(src, this._lastTex);
            Graphics.Blit(src, dest);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
