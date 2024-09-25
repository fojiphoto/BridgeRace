using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMC : MonoBehaviour
{
    private IState<CharacterStateMC> currentState;

    private void Start()
    {
        //ChangeState(new BotFindingState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<CharacterStateMC> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}
