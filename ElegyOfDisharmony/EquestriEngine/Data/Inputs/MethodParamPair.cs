using System;

namespace EquestriEngine.Data.Inputs
{
    public class MethodParamPair : Tuple<GenericEvent,Data.Inputs.Interfaces.IEventInput,bool>
    {
        Collections.MethodParamCollection _collectionReference;

        public bool IsConditional
        {
            get { return Item2 != null; }
        }

        public MethodParamPair(
            GenericEvent genericMethod,
            Interfaces.IEventInput input,
            Collections.MethodParamCollection collection = null,
            bool pause = false)
            :base(genericMethod,input,pause)
        {
            _collectionReference = collection;
        }

        public bool ExecuteMethod(object sender)
        {
            bool result = false;
            if (Item1 != null)
            {
                this.Item1.Invoke(sender, this.Item2);
                result = false;
            }

            return result;
        }
    }
}
