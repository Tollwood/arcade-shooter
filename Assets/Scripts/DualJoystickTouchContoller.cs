/*
about this script: 

if a joystick is not set to stay in a fixed position
 for the left joystick, this script makes it appear and disappear within the left-side half of the screen where the screen was touched 
 for the right joystick, this script makes it appear and  disappear within the right-side half of the screen where the screen was touched 

if a joystick is set to stay in a fixed position
 for the left joystick, this script makes it appear and disappear if the user touches within the area of its background image (even if it is not currently visible)
 for the right joystick, this script makes it appear and disappear if the user touches within the area of its background image (even if it is not currently visible)
 
this script also keeps one or both joysticks always visible
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DualJoystickTouchContoller : MonoBehaviour
{
    public Image leftJoystickBackgroundImage; // background image of the left joystick (the joystick's handle (knob) is a child of this image and moves along with it)
    public Image rightJoystickBackgroundImage; // background image of the right joystick (the joystick's handle (knob) is a child of this image and moves along with it)
    public bool leftJoystickAlwaysVisible = false; // value from left joystick that determines if the left joystick should be always visible or not
    public bool rightJoystickAlwaysVisible = false; // value from right joystick that determines if the right joystick should be always visible or not

    private Image leftJoystickHandleImage; // handle (knob) image of the left joystick
    private Image rightJoystickHandleImage; // handle (knob) image of the right joystick
    private LeftJoystick leftJoystick; // script component attached to the left joystick's background image
    private RightJoystick rightJoystick; // script component attached to the right joystick's background image
    private int leftSideFingerID = 0; // unique finger id for touches on the left-side half of the screen
    private int rightSideFingerID = 0; // unique finger id for touches on the right-side half of the screen

    void Start()
    {

        if (leftJoystickBackgroundImage.GetComponent<LeftJoystick>() == null)
        {
            Debug.LogError("There is no LeftJoystick script attached to the Left Joystick game object.");
        }
        else
        {
            leftJoystick = leftJoystickBackgroundImage.GetComponent<LeftJoystick>(); // gets the left joystick script
            leftJoystickBackgroundImage.enabled = leftJoystickAlwaysVisible; // sets left joystick background image to be always visible or not
        }

        if (leftJoystick.transform.GetChild(0).GetComponent<Image>() == null)
        {
            Debug.LogError("There is no left joystick handle image attached to this script.");
        }
        else
        {
            leftJoystickHandleImage = leftJoystick.transform.GetChild(0).GetComponent<Image>(); // gets the handle (knob) image of the left joystick
            leftJoystickHandleImage.enabled = leftJoystickAlwaysVisible; // sets left joystick handle (knob) image to be always visible or not
        }


        if (rightJoystickBackgroundImage.GetComponent<RightJoystick>() == null)
        {
            Debug.LogError("There is no RightJoystick script attached to Right Joystick game object.");
        }
        else
        {
            rightJoystick = rightJoystickBackgroundImage.GetComponent<RightJoystick>(); // gets the right joystick script
            rightJoystickBackgroundImage.enabled = rightJoystickAlwaysVisible; // sets right joystick background image to be always visible or not
        }

        if (rightJoystick.transform.GetChild(0).GetComponent<Image>() == null)
        {
            Debug.LogError("There is no right joystick handle attached to this script.");
        }
        else
        {
            rightJoystickHandleImage = rightJoystick.transform.GetChild(0).GetComponent<Image>(); // gets the handle (knob) image of the right joystick
            rightJoystickHandleImage.enabled = rightJoystickAlwaysVisible; // sets right joystick handle (knob) image to be always visible or not
        } 
    }

    void Update()
    {
        // can move code from FixedUpdate() to Update() if your controlled object does not use physics
        // can move code from Update() to FixedUpdate() if your controlled object does use physics
        // can see which one works best for your project
    }

    void FixedUpdate()
    {
        // if the screen has been touched
        if (Input.touchCount > 0)
        {
            Touch[] myTouches = Input.touches; // gets all the touches and stores them in an array

            // loops through all the current touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                // if this touch just started (finger is down for the first time), for this particular touch 
                if (myTouches[i].phase == TouchPhase.Began)
                {
                        // if this touch is on the left-side half of screen
                        if (myTouches[i].position.x < Screen.width / 2)
                        {
                            leftSideFingerID = myTouches[i].fingerId; // stores the unique id for this touch that happened on the left-side half of the screen

                            // if the touch happens within the fixed area of the left joystick's background image within the x coordinate
                            if ((myTouches[i].position.x <= leftJoystickBackgroundImage.rectTransform.position.x) && (myTouches[i].position.x >= (leftJoystickBackgroundImage.rectTransform.position.x - leftJoystickBackgroundImage.rectTransform.sizeDelta.x)))
                            {
                                // and the touch also happens within the left joystick's background image y coordinate
                                if ((myTouches[i].position.y >= leftJoystickBackgroundImage.rectTransform.position.y) && (myTouches[i].position.y <= (leftJoystickBackgroundImage.rectTransform.position.y + leftJoystickBackgroundImage.rectTransform.sizeDelta.y)))
                                {
                                    // makes the left joystick appear 
                                    leftJoystickBackgroundImage.enabled = true;
                                    leftJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                                }
                            }
                           
                        }

                    // if this touch is on the right-side half of screen
                    if (myTouches[i].position.x > Screen.width / 2)
                    {
                        rightSideFingerID = myTouches[i].fingerId; // stores the unique id for this touch that happened on the right-side half of the screen

                        // right joystick stays fixed, does not set position of right joystick on touch

                        // if the touch happens within the fixed area of the right joystick's background image within the x coordinate
                        if ((myTouches[i].position.x <= rightJoystickBackgroundImage.rectTransform.position.x) && (myTouches[i].position.x >= (rightJoystickBackgroundImage.rectTransform.position.x - rightJoystickBackgroundImage.rectTransform.sizeDelta.x)))
                        {
                            // and the touch also happens within the right joystick's background image y coordinate
                            if ((myTouches[i].position.y >= rightJoystickBackgroundImage.rectTransform.position.y) && (myTouches[i].position.y <= (rightJoystickBackgroundImage.rectTransform.position.y + rightJoystickBackgroundImage.rectTransform.sizeDelta.y)))
                            {
                                // makes the right joystick appear
                                rightJoystickBackgroundImage.enabled = true;
                                rightJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                            }
                        }
                    }
                }

                // if this touch has ended (finger is up and now off of the screen), for this particular touch 
                if (myTouches[i].phase == TouchPhase.Ended)
                {
                    // if this touch is the touch that began on the left half of the screen
                    if (myTouches[i].fingerId == leftSideFingerID)
                    {
                        // makes the left joystick disappear or stay visible
                        leftJoystickBackgroundImage.enabled = leftJoystickAlwaysVisible;
                        leftJoystickHandleImage.enabled = leftJoystickAlwaysVisible;
                    }

                    // if this touch is the touch that began on the right half of the screen
                    if (myTouches[i].fingerId == rightSideFingerID)
                    {
                        // makes the right joystick disappear or stay visible
                        rightJoystickBackgroundImage.enabled = rightJoystickAlwaysVisible;
                        rightJoystickHandleImage.enabled = rightJoystickAlwaysVisible;
                    }
                }
            }
        }
    }
}
