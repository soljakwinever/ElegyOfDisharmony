using EquestriEngine.Data.Inputs;

namespace EquestriEngine.Data.Collections
{
    public class MethodParamCollection
    {
        //Requires the players input to continue
        private bool requiresInput;

        public bool RequireInput
        {
            get { return requiresInput; }
            set { requiresInput = value; }
        }

        public static void MethodExecuted(object sender, MethodParamPair method, MethodResult result)
        {

        }
    }
}
