using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckStatus
{
    static CheckStatus()
    {
        HasRun = false;
        ClickerHasRun = false;
    }

    public static bool HasRun { get; set; }
    public static bool ClickerHasRun { get; set; }
}
