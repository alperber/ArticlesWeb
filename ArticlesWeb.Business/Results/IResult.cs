using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.Business.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
