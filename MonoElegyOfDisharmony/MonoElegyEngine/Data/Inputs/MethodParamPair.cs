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
        False = 4
    }

    public delegate MethodResult MethodParamResult(object sender, Data.Inputs.Interfaces.IEventInput input);
    public delegate void ExecuteAction(object sender,MethodParamPair method, MethodResult result);

    public class MethodParamPair
    {
        private MethodResult _result;
        private MethodParamResult _method;
        private Interfaces.IEventInput _params;

        private Dictionary<MethodResult, int> _exitPaths;

        public event ExecuteAction OnPostExecute;

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
            Dictionary<MethodResult,int> _paths)
        {
            _result = MethodResult.None;
            _method = method;
            _params = input;
            _exitPaths = _paths;
        }

        public MethodResult ExecuteMethod(object sender)
        {
            MethodResult result = 0;
            if (_method != null)
            {

                result = _method.Invoke(sender, _params);
                if(OnPostExecute != null)
                    OnPostExecute(sender,this,result);
            }
            else
                return MethodResult.Fail;

            return result;
        }
    }
}
