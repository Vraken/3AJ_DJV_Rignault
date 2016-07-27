using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SCRIPT_GameSequence : MonoBehaviour
{
    bool environementOK;
    bool playersOK;
    bool monstersOK;
    bool interactionsOK;

    public UnityEvent StartEnvironment;
    public UnityEvent StartPlayers;
    public UnityEvent StartMonsters;
    public UnityEvent StartInteractions;

    public bool InteractionsOK
    {
        get { return interactionsOK; }
        set { interactionsOK = value; }
    }

    public bool EnvironementOk
    {
        get { return environementOK; }
        set { environementOK = value; }
    }

    public bool PlayersOk
    {
        get { return playersOK; }
        set { playersOK = value; }
    }

    public bool MonstersOk
    {
        get { return monstersOK; }
        set { monstersOK = value; }
    }

    public void Awake()
    {
        InteractionsOK = false;
        EnvironementOk = false;
        PlayersOk = false;
        MonstersOk = false;
        StartCoroutine(GameSequenceCoroutine());
    }

    public IEnumerator GameSequenceCoroutine()
    {
        StartEnvironment.Invoke();
        while (!EnvironementOk)
        {
            yield return null;
        }

        StartPlayers.Invoke();
        while (!PlayersOk)
        {
            yield return null;
        }

        StartMonsters.Invoke();
        while (!MonstersOk)
        {
            yield return null;
        }

        StartInteractions.Invoke();
        while (!InteractionsOK)
        {
            yield return null;
        }       
    }
}
