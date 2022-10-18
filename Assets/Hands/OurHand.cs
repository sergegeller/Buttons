using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OurHand : MonoBehaviour
{
    public GameObject ourHandPrefab;
    public InputDeviceCharacteristics ourControllerCharacteristics;
    public Animator ourHandAnimator;

    private InputDevice ourDevice;
    void Start()
    {
        InitialiazeOurHand();
    }

    // Update is called once per frame
    void InitialiazeOurHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(ourControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            ourDevice = devices[0];

            GameObject newhand = Instantiate(ourHandPrefab, transform);
            ourHandAnimator = newhand.GetComponent<Animator>();
        }
    }

    //update is called once per frame
    void Update()
    {
        //Change Animate position or re-initialize
        if (ourDevice.isValid)
        {
            UpdateOurHand();
        }
        else
        {
            InitialiazeOurHand();
        }
    }
    void UpdateOurHand()
    {
        if (ourDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggervalue))
        {
            ourHandAnimator.SetFloat("Trigger", triggervalue);
        }
        else
        {
            ourHandAnimator.SetFloat("Trigger", 0);
        }

        if (ourDevice.TryGetFeatureValue(CommonUsages.grip, out float gripvalue))
        {
            ourHandAnimator.SetFloat("Grip", gripvalue);
        }

        else
        {
            ourHandAnimator.SetFloat("Grip", 0);
        }
    }
}