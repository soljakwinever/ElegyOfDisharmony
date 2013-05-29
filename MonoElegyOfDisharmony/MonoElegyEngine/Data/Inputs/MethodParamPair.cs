using System;
using System.Collections.Generic;

namespace EquestriEngine.Data.Inputs
{
    public enum MethodResult
    {
        None = 0,
        Success,
        Fail = -1,
        True = 3,
        False = 4,
        Yes = 8,
        No = 9
    }

    public delegate MethodResult MethodParamResult(object sender, Data.Inputs.Interfaces.IEventInput input);
    public delegate void ExecuteAction(object sender, MethodParamPair method, MethodResult result);

    public class MethodParamPair
    {
        private MethodResult _result;
        private MethodParamResult _method;
        private Interfaces.IEventInput _params;

        private Dictionary<MethodResult, int> _exitPaths;

        public event ExecuteAction PostExecuteHandler;

        private object sender;

        public int NextMethod
        {
            get
            {
                if (_exitPaths.ContainsKey(_result))
                    return _exitPaths[_result];
                else
                    return -1;
            }
        }

        public MethodResult Result
        {
            get { return _result; }
        }

        public MethodParamPair(
            MethodParamResult method,
            Interfaces.IEventInput input, int nextMethod)
        {
            _result = MethodResult.None;
            _method = method;
            _params = input;
            _exitPaths = new Dictionary<MethodResult, int>();
            _exitPaths[MethodResult.Success] = nextMethod;
        }

        public MethodParamPair(
    MethodParamResult method,
    Interfaces.IEventInput input,
            Dictionary<MethodResult, int> _paths)
        {
            _result = MethodResult.None;
            _method = method;
            _params = input;
            _exitPaths = _paths;
        }

        public void ExecuteMethod(object sender)
        {
            if (_method != null)
            {
                this.sender = sender;
                IAsyncResult temp;
                temp = _method.BeginInvoke(sender, _params, PostExecute, null);
                //_result = _method.EndInvoke(temp);
            }
        }

        private void PostExecute(IAsyncResult syncResult)
        {
            _result = _method.EndInvoke(syncResult);
            if (PostExecuteHandler != null)
                PostExecuteHandler(sender, this, _result);
        }
    }
}
