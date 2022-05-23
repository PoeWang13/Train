using UnityEngine;
public class ScreenFixing
{
    private static Vector3 levelLimit = Vector3.one;
    public static void SetScreenFixing(float myTargetRatio, float screenRatio, bool landScape = true)
    {
        if (myTargetRatio != screenRatio)
        {
            float newWidth = screenRatio / myTargetRatio;
            if (landScape)
            {
                levelLimit = new Vector3(newWidth, 1, 1);
            }
            else
            {
                levelLimit = new Vector3(1, newWidth, 1);
            }
        }
        else
        {
            Debug.Log("Oran orantı duzgun.");
        }
    }
    public static Vector3 MyScreenRatio()
    {
        return levelLimit;
    }
}