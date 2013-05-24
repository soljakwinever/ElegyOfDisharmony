using System.Collections.Generic;
using EquestriEngine.Data.Inputs;

namespace EquestriEngine.Data.Collections
{
    public class ActionList
    {
        private LinkedList<MethodParamPair> _methods;

        private LinkedListNode<MethodParamPair> _current = null;
        private object _owner = null;

        public MethodParamPair Current
        {
            get { return _current.Value; }
        }

        public void ExecuteCurrent()
        {
            _current.Value.ExecuteMethod(null);
        }

        public ActionList(object owner = null)
        {
            _owner = owner;
            _methods = new LinkedList<MethodParamPair>();
        }

        public void AddMethod(MethodParamPair pair)
        {
            _methods.AddLast(pair);
            if (_current == null)
                _current = _methods.First;
        }
    }
}
