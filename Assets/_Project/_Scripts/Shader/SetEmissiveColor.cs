using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetEmissiveColor : MonoBehaviour
{
    [SerializeField][ColorUsage(true, true)]
    public Color _Color;

    private Material _material;
    private Color _currentColor;

    private void OnEnable()
    {
        _currentColor = _Color;

        //try{_material = GetComponent<SpriteRenderer>().material;}
        //catch (System.Exception) { }

        _material = GetComponent<SpriteRenderer>().material;
        _material.SetColor("_ColorHdr", _currentColor);
    }
    
    void Update()
    {
        if(_currentColor != _Color)
        {
            _currentColor = _Color;
            _material.SetColor("_ColorHdr", _currentColor);
        }
    }
}
