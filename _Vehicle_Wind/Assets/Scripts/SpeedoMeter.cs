using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedoMeter : MonoBehaviour
{
    public Rigidbody rb;
    public float speedVehicle;

    public float minspeed;
    public float maxspeed;

    public Text speedText;
    public RectTransform needle;

    private float speed;

    private void Update()
    {
        speed = rb.velocity.magnitude * 3.624f; //m/s - k/h

        if (speedText != null)
        {
            speedText.text = (int)speed + "km/h";
        }
        if (needle != null)
        {
            needle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minspeed, maxspeed, speed / speedVehicle));

        }
    }

}
