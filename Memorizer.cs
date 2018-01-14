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
        public void ClearMemorizedResults() => this._memorizedResults.Clear();
        public TResult GetMemorizedResult(T param) => this._memorizedResults[param];
        public bool TryGetMemorizedResult(T param, out TResult output) => this._memorizedResults.TryGetValue(param, out output);

        public TResult Invoke(T param)
        {
            if(!IsResultMemorized(param))
            {
                this._memorizedResults[param] = _functionToMemorize(param);
            }
            return this._memorizedResults[param];
        }
    }
}
