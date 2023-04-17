using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestStateMachine : MonoBehaviour ,IStateMachineOwner
{
    public Text text;
    private void Start()
    {
        text = GetComponent<Text>();
        StateMachine machine = new StateMachine();
        machine.Init(this);
        machine.ChangeState<StateA>();
    }

    
}

public class StateA : StateBase
{
    public override void Enter()
    {
        (machine.owner as TestStateMachine).text.text = "A";
    }
    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.machine.ChangeState<StateB>();
        }
    }
}

public class StateB : StateBase
{
    public override void Enter()
    {
        (machine.owner as TestStateMachine).text.text = "B";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.machine.ChangeState<StateA>();
        }
    }
}