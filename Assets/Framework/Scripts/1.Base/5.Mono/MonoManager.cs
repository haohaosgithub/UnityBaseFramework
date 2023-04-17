using System;

namespace Framework
{
    public class MonoManager : ManagerBase<MonoManager>
    {
        public Action updateAction;

        public void AddUpdateListener(Action action)
        {
            updateAction += action;
        }
        
        public void RemoveUpdateListener(Action action)
        {
            updateAction -= action;
        }

        private void Update()
        {
            updateAction?.Invoke();
        }
    }
}

