using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : Health
{
    public static float CurrentShield { get; set; } = 50f;
    public static float MaxShield { get; set; } = 50f;

    public RectTransform shieldBarGauge;

    private float MaxShieldPos
    {
        get
        {
            return 0f;
        }
    }
    private float NoShieldPos
    {
        get
        {
            return -400f;
        }
    }

    public static bool ShieldActive { get; set; }

    protected override void LateUpdate()
    {
        if (CurrentShield > MaxShield) CurrentShield = MaxShield;
        if (CurrentShield <= 0)
        {
            CurrentShield = 0;
            ShieldActive = false;
        }
        else ShieldActive = true;
        MoveBar();
    }

    protected override void MoveBar()
    {
        float currentPos = CalculateFloatBar.Compute(CurrentShield, 0, MaxShield, NoShieldPos, MaxShieldPos);
        shieldBarGauge.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, currentPos, 420f);       
    }
}
