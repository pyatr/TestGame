using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Coroutine delay templates
/// </summary>
public static class CoroutineDelays
{
    public readonly static WaitForEndOfFrame WaitForEndFrame = new();
    public readonly static WaitForFixedUpdate WaitForFixedUpdate = new();

    public readonly static WaitForSeconds OneTenthSecond = new(1 / 10f);
    public readonly static WaitForSeconds OneEighthSecond = new(1 / 8f);
    public readonly static WaitForSeconds OneFourthSecond = new(1 / 4f);
    public readonly static WaitForSeconds HalfSecond = new(1 / 2f);
    public readonly static WaitForSeconds ThreeFourthSecond = new(3 / 4f);
    public readonly static WaitForSeconds Second = new(1f);
    public readonly static WaitForSeconds TwoSeconds = new(2f);
    public readonly static WaitForSeconds ThreeSeconds = new(3f);
    public readonly static WaitForSeconds FourSeconds = new(4f);
    public readonly static WaitForSeconds FiveSeconds = new(5f);
    public readonly static WaitForSeconds TenSeconds = new(10f);
    public readonly static WaitForSeconds Minute = new(60f);
    public readonly static WaitForSeconds Hour = new(3600f);

    public readonly static WaitForSecondsRealtime OneTenthSecondRealtime = new(1 / 10f);
    public readonly static WaitForSecondsRealtime OneEighthSecondRealtime = new(1 / 8f);
    public readonly static WaitForSecondsRealtime OneFourthSecondRealtime = new(1 / 4f);
    public readonly static WaitForSecondsRealtime HalfSecondRealtime = new(1 / 2f);
    public readonly static WaitForSecondsRealtime ThreeFourthSecondRealtime = new(3 / 4f);
    public readonly static WaitForSecondsRealtime SecondRealtime = new(1f);
    public readonly static WaitForSecondsRealtime TwoSecondsRealtime = new(2f);
    public readonly static WaitForSecondsRealtime ThreeSecondsRealtime = new(3f);
    public readonly static WaitForSecondsRealtime FourSecondsRealtime = new(4f);
    public readonly static WaitForSecondsRealtime FiveSecondsRealtime = new(5f);
    public readonly static WaitForSecondsRealtime TenSecondsRealtime = new(10f);
    public readonly static WaitForSecondsRealtime MinuteRealtime = new(60f);
    public readonly static WaitForSecondsRealtime HourRealtime = new(3600f);
}