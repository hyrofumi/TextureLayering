using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Circle : MonoBehaviour
{
    [SerializeField] float _r;

    [SerializeField, Range(0.0f, 10.0f)] float _speed = 0.1f;

    void Start()
    {

    }

    void Update()
    {
        Vector3 p = Vector3.zero;
        float time = Time.time * this._speed;
        p.x = this._r * Mathf.Cos(time);
        p.y = this._r * Mathf.Sin(time);

        this.transform.localPosition = p;
    }
}
