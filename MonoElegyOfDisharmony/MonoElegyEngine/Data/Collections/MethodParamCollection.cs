using EquestriEngine.Data.Inputs;
using System.Collections.Generic;

namespace EquestriEngine.Data.Collections
{
    public class MethodParamCollection
    {
        private List<MethodParamPair> _methods;
        //Requires the players input to continue
        private bool requiresInput;

        private int _next;

        public bool RequireInput
        {
            get { return requiresInput; }
            set { requiresInput = value; }
        }

        public MethodParamCollection()
        {
            _methods = new List<MethodParamPair>();
            _next = 0;
        }

        public void MethodExecuted(object sender, MethodParamPair method, MethodResult result)
        {
            _next = method.NextMethod;
            Execute(sender);
        }

        public void ExecuteFromStart(object sender)
        {
            _next = 0;
            _methods[_next].ExecuteMethod(sender);
        }

        public void Execute(object sender)
        {
            if(_next != -1)
                _methods[_next].ExecuteMethod(sender);
        }

        public void AddMethod(MethodParamPair method)
        {
            method.PostExecuteHandler += MethodExecuted;
            _methods.Add(method);
        }
    }
}
