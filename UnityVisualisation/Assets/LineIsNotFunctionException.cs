using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public class LineIsNotFunctionException : Exception
{
    public LineIsNotFunctionException() : base("Linia jest równoległa do osi Y a -> inf") { }
    public LineIsNotFunctionException(Line line) : base(line + " jest równoległa do osi Y a -> inf") { }

    public LineIsNotFunctionException(string message) : base(message) { }
    public LineIsNotFunctionException(string message, Exception inner) : base(message, inner) { }
    protected LineIsNotFunctionException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}