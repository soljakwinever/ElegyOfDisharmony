namespace EquestriEngine.Data.Inputs
{
    public struct IntInput : Interfaces.IEventInput
    {
        private int intInput;

        public int Input
        {
            get { return intInput; }
            set { intInput = value; }
        }
    }
}
