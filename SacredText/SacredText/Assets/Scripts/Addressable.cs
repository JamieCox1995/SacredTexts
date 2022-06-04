using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Addressable : MonoBehaviour, IAddressable
{
    public string Name;

    private float currentMouseOverTime = 0f;
    private const float mouseOverThreshold = 1.5f;

    private bool displayInformation = false;

    public void SetName(string _Value)
    {
        Name = _Value;
    }

    public string GetName()
    {
        return Name;
    }

    public virtual string GetAddressableTooltip()
    {
        return Name;
    }

    protected virtual void OnMouseOver()
    {
        if (currentMouseOverTime >= mouseOverThreshold)
        {
            displayInformation = true;
            currentMouseOverTime = mouseOverThreshold;

            WorldInteractionSystem.Instance.OnDisplayMouseOverInformation(this);
        }
        else
        {
            currentMouseOverTime += Time.deltaTime;
        }
    }

    protected void OnMouseExit()
    {
        displayInformation = false;
        currentMouseOverTime = 0f;

        WorldInteractionSystem.Instance.OnHideMouseOverInformation();
    }
}