using EquestriEngine.Data.Inputs;
using System.Collections.Generic;

namespace EquestriEngine.Data.Collections
{
    public class MethodParamCollection
    {
        private MethodParamPair[] _methods;
        //Requires the players input to continue

        private int _next;

        public MethodParamCollection()
        {
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
        }

        public static void LoadScriptFromFile(out MethodParamCollection script, string fileName)
        {
            script = new MethodParamCollection();
            using (Utilities.FormattedFile file = new Utilities.FormattedFile())
            {
                file.ReadBegin(fileName);

                if (file.ReadBlock() == "Script")
                {
                    int count = -1;
                    count = int.Parse(file.ReadLine());

                    script._methods = new MethodParamPair[count];

                    for (int i = 0; i < count; i++)
                    {
                        MethodParamPair method;
                        string methodName = file.ReadBlock();
                        int paramNum = int.Parse(file.ReadLine());
                        string[] tempParams = new string[paramNum];
                        for (int j = 0; j < paramNum; j++)
                        {
                            tempParams[j] = file.ReadLine();
                        }
                        int exitNum = int.Parse(file.ReadLine());
                        string[] tempExits = new string[exitNum];
                        for (int j = 0; j < exitNum; j++)
                        {
                            tempExits[j] = file.ReadLine();
                        }
                        file.ReadEndBlock();
                        method = EngineGlobals.GenerateMethodFromArgs(methodName, tempParams, tempExits);
                        method.PostExecuteHandler += script.MethodExecuted;
                        script._methods[i] = method;

                    }
                    file.ReadEndBlock();
                    file.ReadEnd();
                }
                else
                    throw new EngineException("Could not load script " + fileName, false);
            }
        }
    }
}
