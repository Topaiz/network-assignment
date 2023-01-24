using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTestCamera : MonoBehaviour
{
    private Transform camTransform;

    [SerializeField]
    public float followSpeed = 1.0f;

    // The time at which the animation started.
    private float startTime;

    GameObject playerino;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = transform;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerino != null)
        {
            UpdatePosition();

        }
        
    }

    public void SetPlayer(GameObject player)
    {
        playerino = player;
    }

    void UpdatePosition()
    {
        //float fracComplete = (Time.time - startTime) / followSpeed;
        float t = Time.time / startTime; //time / duration;
        //t = t * t * (3f - 2f * t);

        Vector3 newPos = new Vector3(playerino.transform.position.x,
            playerino.transform.position.y, camTransform.position.z);

        camTransform.position = Vector3.Lerp(
            camTransform.position, newPos, Time.deltaTime * followSpeed);

    }
}
