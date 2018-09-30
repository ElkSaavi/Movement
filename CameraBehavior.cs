using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : CharacterController
{
	private Transform camTf;
    private float t, k, c, d;
    private float currentHeight, standardHeight;

	public float sensitivity = 1 , sensitivityMultiplier = 1;
    
	private void Start()
	{
		camTf = cam.GetComponent<Transform>();
		hRot = vRot = 0;
        standardHeight = camTf.localPosition.y;
	}
	private void Update()
	{
		hRot += Input.GetAxisRaw("Mouse X") * sensitivity * sensitivityMultiplier;
		vRot += Input.GetAxisRaw("Mouse Y") * -sensitivity * sensitivityMultiplier;

		// add dip before jump
	}
	private void FixedUpdate()
	{
		Look();

        if (impact && impactSpeed > 6 && !dipping)
        {
            dipping = true;
            StartCoroutine("HeadDip");
        }
    }
	private void Look()
	{
		hRot %= 360;
		if (hRot < 0)
			hRot += 360;

		vRot = Mathf.Clamp(vRot, -90, 90);

		camTf.localEulerAngles = new Vector3(vRot,0,0);
		tf.localEulerAngles = new Vector3(0,hRot,0);
	}
    IEnumerator DipHead()    // find max depth point and enable jump charging from there... y'no
    {
        t = 0;
        // d = something something impactSpeed;
        while (true)
        {
            t += Time.fixedDeltaTime;

            if (t >= c / k)
            {
                currentHeight = standardHeight;
                camTf.localPosition = new Vector3(0, currentHeight, 0);
                t = 0;
                dipping = false;
                yield break;
            }
            else
            {
                currentHeight = standardHeight + (-d * k * t * Mathf.Pow((k * t - c), 4));
                camTf.localPosition = new Vector3(0, currentHeight, 0);
                yield return null;
            }
        }
    }
}