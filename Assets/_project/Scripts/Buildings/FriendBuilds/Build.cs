using UnityEngine;

public abstract class Build : MonoBehaviour
{
    public bool IsNeedToBuild { get; private set; } = true;

    public abstract void Click();
}
