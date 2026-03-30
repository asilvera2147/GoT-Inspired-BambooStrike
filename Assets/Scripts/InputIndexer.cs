using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputIndexer : MonoBehaviour
{
    public List<String> inputs = new List<String>();
    public static InputIndexer instance;
    private MainInputActionMap inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        inputActions.Player.Enable();
    }
    void OnDisable()
    {
        inputActions.Player.Disable();
    }
    
    void Awake()
    {
        inputActions = new MainInputActionMap();
    }

    void Start()
    {
        if (instance == null)
            instance = this;

        inputActions.Player.X.performed += XPerformed;
        inputActions.Player.Triangle.performed += TrianglePerformed;
        inputActions.Player.Circle.performed += CirclePerformed;
        inputActions.Player.Square.performed += SquarePerformed;
        inputActions.Player.L1.performed += L1Performed;
        inputActions.Player.R1.performed += R1Performed;
    }

    private void R1Performed(InputAction.CallbackContext context)
    {
        inputs.Add("R1");
    }

    private void L1Performed(InputAction.CallbackContext context)
    {
        inputs.Add("L1");
    }

    private void SquarePerformed(InputAction.CallbackContext context)
    {
        inputs.Add("Square");
    }

    private void CirclePerformed(InputAction.CallbackContext context)
    {
        inputs.Add("Circle");
    }

    private void TrianglePerformed(InputAction.CallbackContext context)
    {
        inputs.Add("Triangle");
    }

    private void XPerformed(InputAction.CallbackContext context)
    {
        inputs.Add("X");

    }

    public void ResetList()
    {
        inputs.Clear();
    }

}
