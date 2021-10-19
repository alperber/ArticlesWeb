using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.Business.Results
{
    // TODO  out olmadan dene
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }//readonly
    }
}
