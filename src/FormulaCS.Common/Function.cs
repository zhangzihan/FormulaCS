namespace FormulaCS.Common
{
    public class Function
    {
        public FunctionDelegate Delegate { get; set; }
        public bool IsThreadSafe { get; set; }
    }
}