using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected static RaycastHit groundCheck;
    protected static float hInput, vInput;
    protected static float hRot, vRot;
    protected static Vector3 side, forward;
    protected static Vector3 hVelocity;
    protected static float slope;
    protected static float impactSpeed;
    protected static bool grounded, impact, sliding, dipping, hooked;

    [Header("Components")]
    public static Rigidbody rb;
    public static Transform tf;
    public static Camera cam;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (tf == null)
            tf = GetComponent<Transform>();
        if (cam == null)
            cam = GetComponentInChildren<Camera>();
    }
}