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
    [SerializeField] private Transform _character;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _axis;
 
    private void Update()
    {
        // Smoothly fade background between given colours
        _lerpColour = Color.Lerp(_color1, _color2, Mathf.PingPong(Time.time, 1));
        _backgroundRenderer.color = _lerpColour;
        
        RotateWithChar();
    }

    /// <summary>
    /// Rotating preview model 
    /// </summary>
    void RotateWithChar()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // Getting input
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _rotationSpeed;

            //Creating Vector3
            Vector3 dir = new Vector3(0, -mouseX, 0);
            //Rotating preview model around X axis of parent by mouse
            _character.RotateAround(_axis.position, dir, dir.magnitude * _rotationSpeed);
        }
    }

    /// <summary>
    ///  Function for quiting aplication
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
