using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace DynamicFilter.Sql.Parser
{
    internal class DynamicFilterErrorListener : BaseErrorListener
    {
        public List<Error> Errors { get; set; } = new List<Error>();

        public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            Errors.Add(new Error { Message = msg, LineNumber = line, Position = charPositionInLine});
        }
    }
}
