// From: https://forum.unity.com/threads/ability-to-add-enum-argument-to-button-functions.270817/
// Author: llamagod

/*
Usage:

Place the UnityEventDrawer in your Editor folder (Assets/Editor), place EnumActionAttribute anywhere you like.
When there is a method with one enum argument you'd like to expose in the UnityEvent create a method taking in
an integer argument and apply the EnumActionAttribute to it, passing in your enum type.

If you do that the integer method will be available in the UnityEvent inspector, but now the argument will be shown as an enum dropdown.

public class SomeClass : MonoBehaviour
{
    [EnumAction(typeof(SomeEnum))]
    public void SomeMethod(int argument)
    {
        var enumArgument = (SomeEnum)argument;
        // Implement method
    }
}

*/

using System;
using UnityEngine;

/// <summary>
/// Mark a method with an integer argument with this to display the argument as an enum popup in the UnityEvent
/// drawer. Use: [EnumAction(typeof(SomeEnumType))]
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class EnumActionAttribute : PropertyAttribute
{
    public Type enumType;

    public EnumActionAttribute(Type enumType)
    {
        this.enumType = enumType;
    }
}
