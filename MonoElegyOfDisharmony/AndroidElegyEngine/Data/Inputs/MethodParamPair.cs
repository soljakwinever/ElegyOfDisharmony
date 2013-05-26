using System;

namespace EquestriEngine.Data.Inputs
{
    public enum MethodResult
    {
        None = 0,
        Success,
        Fail = -1,
        True,
        False
    }

    public delegate MethodResult MethodParamResult(object sender,Data.Inputs.Interfaces.IEventInput input);
    public delegate void ExecuteAction(object sender,MethodResult result);

    public class MethodParamPair
    {
        private MethodParamResult _method;
        private Interfaces.IEventInput _params;

        public event ExecuteAction OnPostExecute;

        public MethodParamPair(
            MethodParamResult method,
            Interfaces.IEventInput input)
        {
            _method = method;
            _params = input;
        }

        public MethodResult ExecuteMethod(object sender)
        {
            MethodResult result = 0;
            if (_method != null)
            {

                result = _method.Invoke(sender, _params);
            }
            else
                return MethodResult.Fail;

            return result;
        }
    }
}
