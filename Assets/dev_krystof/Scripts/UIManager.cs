using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Color _lerpColour;
    [SerializeField] private Image _backgroundRenderer;
    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    [SerializeField] private Transform _characterX;
    [SerializeField] private Transform _characterY;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _finalRot;
 
    private void Update()
    {
        _lerpColour = Color.Lerp(_color1, _color2, Mathf.PingPong(Time.time, 1));
        _backgroundRenderer.color = _lerpColour;
        
        RotateWithChar();
    }

    void RotateWithChar()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _rotationSpeed;

            Vector3 dirX = new Vector3(0, -mouseX, 0);
            Vector3 dirY = new Vector3(mouseY, 0, 0);

            _characterX.eulerAngles += dirX;
            _characterY.eulerAngles += dirY;

            _finalRot.eulerAngles = _characterX.rotation.eulerAngles + _characterY.rotation.eulerAngles;
        }
    }
}
