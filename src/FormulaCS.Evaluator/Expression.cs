using FormulaCS.Common;
using FormulaCS.Parser;

namespace FormulaCS.Evaluator
{
    public class Expression : IExpression
    {
        private readonly FormulaParser.ExprContext _context;
        private readonly EvaluationVisitor _visitor;

        public Expression(FormulaParser.ExprContext context, EvaluationVisitor visitor)
        {
            _context = context;
            _visitor = visitor;
        }

        public object Evaluate()
        {
            return _context.Accept(_visitor);
        }
    }
}