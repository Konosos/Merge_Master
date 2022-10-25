using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    #region Singleton
    public static TutorialManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public enum TutorialStates { State1, State2, State3}
    public TutorialStates tutorialStates;

    public static System.Action EndStep1 = delegate { };
    public static System.Action EndStep2 = delegate { };
    public static System.Action EndStep3 = delegate { };
    public static System.Action EndStep4 = delegate { };
}
