using System;

namespace EquestriEngine.Data.Inputs
{
    public class MethodParamPair : Tuple<GenericEvent,ConditionalEvent,Data.Inputs.Interfaces.IEventInput,bool>
    {
        Collections.MethodParamCollection _collectionReference;

        public bool IsConditional
        {
            get { return Item2 != null; }
        }

        public MethodParamPair(
            GenericEvent genericMethod,
            ConditionalEvent conditionalMethod,
            Interfaces.IEventInput input,
            Collections.MethodParamCollection collection = null,
            bool pause = false)
            :base(genericMethod,conditionalMethod,input,pause)
        {
            _collectionReference = collection;
            if ((genericMethod != null && conditionalMethod != null) ^
                (genericMethod == null && conditionalMethod == null))
                throw new Data.Exceptions.EngineException("Error creating Method Param pair", true);
        }

        public bool ExecuteMethod(object sender)
        {
            bool result = false;
            if (Item1 != null)
            {
                this.Item1.Invoke(sender, this.Item3);
                result = false;
            }
            if (Item2 != null)
                result = this.Item2.Invoke(sender, this.Item3);
            if (Item4 && _collectionReference != null)
                _collectionReference.RequireInput = true;

            return result;
        }
    }
}
