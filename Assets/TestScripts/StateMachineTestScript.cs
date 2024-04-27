using System.Collections;
using UnityEngine;
using StateMachineSLS;
using Vector3Helper;
using TrigHelper;

public class StateMachineTestScript : MonoBehaviour
{

    private void Start()
    {
    }


    public class MyStateMachine : StateMachine
    {
        public MyStateMachine(MonoBehaviour owner) : base(owner) {}

        public new enum State { FirstState, SecondState };
        protected override void InitializeStates()
        {

            RegisterState(new FirstState(owner));
            RegisterState(new SecondState(owner));

        }

        public class FirstState : StateBase
        {
            public FirstState(MonoBehaviour owner) : base(owner){}
        }

        public class SecondState : StateBase
        {
            public SecondState(MonoBehaviour owner) : base(owner){}
        }







    }
}
