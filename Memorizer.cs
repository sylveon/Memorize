using System;
using System.Collections.Generic;

namespace Sylveon.Memorize
{
    public class Memorizer<T, TResult>
    {
        private Func<T, TResult> _functionToMemorize;

        private Dictionary<T, TResult> _memorizedResults = new Dictionary<T, TResult>();

        public Memorizer(Func<T, TResult> func) => this._functionToMemorize = func;

        public bool IsResultMemorized(T param) => this._memorizedResults.ContainsKey(param);

        public TResult Invoke(T param)
        {
            if(!IsResultMemorized(param))
            {
                this._memorizedResults[param] = _functionToMemorize(param);
            }
            return this._memorizedResults[param];
        }

        public bool GetMemorizedResult(T param, out TResult output)
        {
            if(IsResultMemorized(param))
            {
                output = this._memorizedResults[param];
                return true;
            }
            else
            {
                output = default;
                return false;
            }
        }

        public void ClearMemorizedResults() => this._memorizedResults.Clear();
    }
}
