using System;

namespace Framework
{
    public class MonoManager : ManagerBase<MonoManager>
    {
        public Action updateAction;
        public Action lateUpdateAction;
        public Action fixedUpdateAction;
        public void AddUpdateListener(Action action)
        {
            updateAction += action;
        }
        
        public void RemoveUpdateListener(Action action)
        {
            updateAction -= action;
        }

        public void AddLateUpdateListener(Action action)
        {
            lateUpdateAction += action;
        }

        public void RemoveLateUpdateListener(Action action)
        {
            lateUpdateAction -= action;
        }

        public void AddFixedUpdateListener(Action action)
        {
            fixedUpdateAction += action;
        }
        public void RemoveFixedUpdateListener(Action action)
        {
            fixedUpdateAction -= action;
        }
        private void Update()
        {
            updateAction?.Invoke();
        }

        private void LateUpdate()
        {
            lateUpdateAction?.Invoke();
        }

        private void FixedUpdate()
        {
            fixedUpdateAction?.Invoke();
        }
    }
}

