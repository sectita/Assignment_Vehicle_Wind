using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindForce : MonoBehaviour
{
    public float windMin = 25f;
    public float windMax = 30f;
    public float windForce;

    private Vector3 windDirection;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Text textComponent;

    private void Start()
    {
        boxCollider = FindObjectOfType<BoxCollider>();

        windDirection = new Vector3(0, 0, 1f);

        Random.Range(windMax, windMin);
    }

    private void FixedUpdate()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxCollider.size / 2f, windDirection, Quaternion.identity, 0f);

        foreach (RaycastHit hit in hits)
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(windDirection * windForce, ForceMode.Force);
            }
        }

        if (Time.fixedTime % 0.5f == 0)
        {
            windForce = Random.Range(windMax, windMin);
            textComponent.text = "Air Speed :" + Mathf.Round(windForce) + "km/h";
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);

    }
    //void Update()
    //{
    //    windDirection = Quaternion.Euler(0, Time.deltaTime, 0) * windDirection;
    //    //windForce = Mathf.Lerp(windForce, Random.Range(windMin, windMax), Time.deltaTime);

    //    //textComponent.text = "Air Speed :" + Mathf.Round(windForce) + "km/h";
    //    //textComponent1.text = "Air Speed :" + windForce + "m/s";
    //}

}
