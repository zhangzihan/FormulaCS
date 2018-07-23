namespace FormulaCS.Common
{
    public interface IFunction
    {
        void Function(IFunctionArgs args, IExcelCaller caller);
    }
}