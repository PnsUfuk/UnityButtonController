using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Include;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ButtonController : MonoBehaviour
{
    private EventTrigger eventTrigger;
    public ButtonState activeState = ButtonState.non;
    public float pressTime = 0f;

    private bool waitForOneFream = false;
    
    private void Awake(){
        eventTrigger = gameObject.GetComponent<EventTrigger>();

        EventTrigger.Entry downEntry = new EventTrigger.Entry();
        downEntry.eventID = EventTriggerType.PointerDown;
        downEntry.callback.AddListener( (eventData) => { Button_Down(); });
        eventTrigger.triggers.Add(downEntry);

        EventTrigger.Entry upEntry = new EventTrigger.Entry();
        upEntry.eventID = EventTriggerType.PointerUp;
        upEntry.callback.AddListener( (eventData) => { Button_Up(); });
        eventTrigger.triggers.Add(upEntry);
    }

    public void Update()
    {
        if (activeState == ButtonState.Down)
        {
            if (!waitForOneFream)
            {
                activeState = ButtonState.Press;
            }
        }

        if (activeState == ButtonState.Press){
            pressTime += Time.deltaTime;
            Debug.Log("Press");
        }
    }

    public void LateUpdate()
    {
        if (waitForOneFream)
            waitForOneFream = false;

        if (activeState == ButtonState.Up)
        {
            pressTime = 0f;
            activeState = ButtonState.non;
        }
    }

    public void Button_Down()//pointter clicks element
    {
        //start timing
        activeState = ButtonState.Down;
        Debug.Log("Down");
    }

    public void Button_Up()//pointer stops clicking
    {
        //end timing        
        activeState = ButtonState.Up;
        Debug.Log("Up");
    }
}
