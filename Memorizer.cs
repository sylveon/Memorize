using System;
using System.Collections.Generic;

namespace Sylveon.Memorize
{
    /// <summary>
    /// A class used to memoize a single-parameter function. Unrecommended,
    /// use the Memorizer<T, TResult> and NullableMemorizer<T, Result>
    /// wrappers instead.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the parameter of the function
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the return value of the function
    /// </typeparam>
    /// <typeparam>
    /// The type of the Dictionary to use.
    /// </typeparam>
    public class Memorizer<T, TResult, TDictionary> where TDictionary : IDictionary<T, TResult>, new()
    {
        private Func<T, TResult> _functionToMemorize;
        private TDictionary _memorizedResults = new TDictionary();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="func">
        /// The function to memoize.
        /// </param>
        public Memorizer(Func<T, TResult> func) => this._functionToMemorize = func;

        /// <summary>
        /// Determines whether the result associated to a parameter has been cached.
        /// </summary>
        /// <param name="param">
        /// The parameter to verify.
        /// </param>
        /// <returns>
        /// true if the result has been cached; otherwise, false.
        /// </returns>
        public bool IsResultMemorized(T param) => this._memorizedResults.ContainsKey(param);

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearMemorizedResults() => this._memorizedResults.Clear();

        /// <summary>
        /// Gets the cached result associated with the specified input parameter.
        /// </summary>
        /// <param name="param">
        /// The input parameter of the result to get.
        /// </param>
        /// <returns>
        /// The result associated with the specified parameter. If the specified key is
        /// not found, throws a System.Collections.Generic.KeyNotFoundException.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// The result has not been cached yet.
        /// </exception>
        public TResult GetMemorizedResult(T param) => this._memorizedResults[param];

        /// <summary>
        /// Gets the cached result associated with the specified input parameter.
        /// </summary>
        /// <param name="param">
        /// The input parameter of the result to get.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains the results associated with the
        /// specified parameter, if it was cached; otherwise, the default value
        /// for the type of the result. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if the result has been cached; otherwise, false.
        /// </returns>
        public bool TryGetMemorizedResult(T param, out TResult result) => this._memorizedResults.TryGetValue(param, out result);

        /// <summary>
        /// Return the cached result if cached; otherwise call the function, cache the
        /// result, and return it.
        /// </summary>
        /// <param name="param">
        /// The parameter that the function must be invoked with
        /// </param>
        /// <returns>
        /// The cached result if cached; otherwise the result of calling the function.
        /// </return>
        public TResult Invoke(T param)
        {
            if(!this.IsResultMemorized(param))
            {
                this._memorizedResults[param] = _functionToMemorize(param);
            }
            return this._memorizedResults[param];
        }
    }

    /// <summary>
    /// A wrapper for the main Memorizer class taking a non-nullable value only.
    /// If you need one handling nullable values, use NullableMemorizer<T, TResult>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the parameter of the function
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the return value of the function
    /// </typeparam>
    /// <seealso cref="Memorizer<T, TResult, TDictionary>" />
    /// <seealso cref="NullableMemorizer<T, TResult>" />
    public class Memorizer<T, TResult> : Memorizer<T, TResult, Dictionary<T, TResult>> where T : struct
    {
        public Memorizer(Func<T, TResult> func) : base(func) { }
    }

    /// <summary>
    /// A wrapper for the main Memorizer class taking a nullable value only.
    /// If you need one handling non-nullable values, use Memorizer<T, TResult>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the parameter of the function
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the return value of the function
    /// </typeparam>
    /// <seealso cref="Memorizer<T, TResult, TDictionary>" />
    /// <seealso cref="Memorizer<T, TResult>" />
    public class NullableMemorizer<T, TResult> : Memorizer<T, TResult, NullableDictionary<T, TResult>> where T : class
    {
        public NullableMemorizer(Func<T, TResult> func) : base(func) { }
    }
}
