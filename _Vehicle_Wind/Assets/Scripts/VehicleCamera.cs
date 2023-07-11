using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    public float move;
    public float rotate;

    public Vector3 moveOffSet;
    public Vector3 rotateOffSet;

    public Transform vehicleTrans;

    private void FixedUpdate()
    {
        VehicleFollow();
    }

    void VehicleFollow()
    {
        //LookPosition
        Vector3 vehiclePos = new Vector3();

        vehiclePos = vehicleTrans.TransformPoint(moveOffSet);

        transform.position = Vector3.Lerp(transform.position, vehiclePos, move * Time.deltaTime);

        //Lookrotate
        var Lookdir = vehicleTrans.position - transform.position;
        var Lookrot = new Quaternion();
        Lookrot = Quaternion.LookRotation(Lookdir + rotateOffSet, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, Lookrot, rotate * Time.deltaTime);

    }
}