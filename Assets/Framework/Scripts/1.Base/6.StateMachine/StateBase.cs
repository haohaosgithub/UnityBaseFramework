namespace Framework
{
    public abstract class StateBase
    {
        public StateMachine machine;
        public virtual void Init(StateMachine machine)
        {
            this.machine = machine;
        }
        
        public virtual void UnInit()
        {

        }

        public virtual void Enter()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void LateUpdate()
        {

        }
        public virtual void FixedUpdate()
        {

        }
        public virtual void Exit()
        {

        }
    }
}

