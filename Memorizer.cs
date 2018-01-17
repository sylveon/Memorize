using System;
using System.Collections.Generic;

namespace Sylveon.Memorize
{
    /// <summary>
    /// A class used to memoize a single-parameter function.
    /// </summary>
    /// <typeparam name="TParam">
    /// The type of the parameter of the function
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the return value of the function
    /// </typeparam>
    public class Memorizer<TParam, TResult>
    {
        private readonly Func<TParam, TResult> _functionToMemorize;
        private readonly NullableDictionary<TParam, TResult> _memorizedResults = new NullableDictionary<TParam, TResult>();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="func">
        /// The function to memoize.
        /// </param>
        public Memorizer(Func<TParam, TResult> func) => _functionToMemorize = func;

        /// <summary>
        /// Determines whether the result associated to a parameter has been memorized.
        /// </summary>
        /// <param name="param">
        /// The parameter to verify.
        /// </param>
        /// <returns>
        /// true if the result has been memorized; otherwise, false.
        /// </returns>
        public bool IsResultMemorized(TParam param) => _memorizedResults.ContainsKey(param);

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearMemorizedResults() => _memorizedResults.Clear();

        /// <summary>
        /// Gets the memorized result associated with the specified input parameter.
        /// </summary>
        /// <param name="param">
        /// The input parameter of the result to get.
        /// </param>
        /// <returns>
        /// The result associated with the specified parameter. If the specified key is
        /// not found, throws a <see cref="KeyNotFoundException"/>.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// The result has not been memorized yet.
        /// </exception>
        public TResult GetMemorizedResult(TParam param) => _memorizedResults[param];

        /// <summary>
        /// Gets the memorized result associated with the specified input parameter.
        /// </summary>
        /// <param name="param">
        /// The input parameter of the result to get.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains the results associated with the
        /// specified parameter, if it was memorized; otherwise, the default value
        /// for the type of the result. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if the result has been memorized; otherwise, false.
        /// </returns>
        public bool TryGetMemorizedResult(TParam param, out TResult result) => _memorizedResults.TryGetValue(param, out result);

        /// <summary>
        /// Return the memorized result if memorized; otherwise call the function, memorize the
        /// result, and return it.
        /// </summary>
        /// <param name="param">
        /// The parameter that the function must be invoked with
        /// </param>
        /// <returns>
        /// The memorized result if memorized; otherwise the result of calling the function.
        /// </returns>
        public TResult Invoke(TParam param)
        {
            if(IsResultMemorized(param))
            {
                return _memorizedResults[param];
            }
            else
            {
                return _memorizedResults[param] = _functionToMemorize(param);
            }
        }
    }
}
