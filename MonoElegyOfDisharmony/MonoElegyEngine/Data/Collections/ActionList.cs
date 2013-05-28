using System.Collections.Generic;
using EquestriEngine.Data.Inputs;

namespace EquestriEngine.Data.Collections
{
    public class ActionList
    {
        private bool _finished;
        private ActionList _subList;
        private LinkedList<MethodParamPair> _methods;
        private LinkedListNode<MethodParamPair> _current = null;
        private object _owner = null;

        public event GenericEvent OnFinish;

        public bool Finished
        {
            get { return _finished; }
        }

        public MethodParamPair Current
        {
            get { return _current.Value; }
        }

        public int Count
        {
            get { return _methods.Count; }
        }

        public ActionList(object owner)
        {
            _owner = owner;
            _methods = new LinkedList<MethodParamPair>();
            _finished = true;
        }

        public void StartFromBeginning()
        {
            _finished = false;
            _current = _methods.First;
            ExecuteCurrent();
        }

        public void ExecuteCurrent()
        {
            if (_subList != null && !_subList.Finished)
            {
                 _subList.ExecuteCurrent();
            }
            else
            {
                var method = _current;
                if (_current.Next != null)
                    _current = _current.Next;
                else
                {
                    _finished = true;
                    if (OnFinish != null)
                        OnFinish(_owner, null);
                }
                _subList = null;
                method.Value.ExecuteMethod(_owner);

            }

        }

        public void AddMethod(MethodParamPair pair)
        {
            _methods.AddLast(pair);
            if (_current == null)
                _current = _methods.First;
        }

        public void AddMethod(MethodParamResult inMethod,Data.Inputs.Interfaces.IEventInput input)
        {
            MethodParamPair pair = new MethodParamPair(inMethod, input,0);
            _methods.AddLast(pair);
            if (_current == null)
                _current = _methods.First;
        }

        public void ExecuteList(ActionList list)
        {
            _subList = list;
            _subList.StartFromBeginning();
        }
    }
}
