using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject hint;
    [SerializeField] private float openAngle = -90.0f;   // The angle at which the door should open
    [SerializeField] private Transform doorTransform; // The transform of the specific door that this script should control
    private float smooth = 2.0f;   // The smoothness of the door's movement
    private bool isOpen = false;    // Whether the door is currently open or not
    private float timeOpen = 0f;    // The time since the door was opened
    private Quaternion initialRotation; // The initial rotation of the door

    void Start()
    {
        doorTransform = transform;
        initialRotation = doorTransform.localRotation;
        hint.SetActive(false); // Disable the hint at the start
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && IsPlayerNearDoor())
        {
            isOpen = !isOpen;
            timeOpen = Time.time;

            if (isOpen)
            {
                hint.SetActive(false); // Disable the hint when the door is opened
            }
        }

        if (isOpen && (Time.time - timeOpen) > 5f)
        {
            isOpen = false;
            hint.SetActive(false); // Enable the hint when the door is closed
        }

        if (isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, openAngle, initialRotation.eulerAngles.z);
            doorTransform.localRotation = Quaternion.Slerp(doorTransform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            doorTransform.localRotation = Quaternion.Slerp(doorTransform.localRotation, initialRotation, smooth * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            hint.SetActive(true); // Enable the hint when the player is near and the door is closed
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hint.SetActive(false); // Disable the hint when the player is no longer near
        }
    }

    bool IsPlayerNearDoor()
    {
        float distance = Vector3.Distance(doorTransform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        return distance <= 2f && !isOpen; // Return true only if the player is within range and the door is closed
    }
}