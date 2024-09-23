

using System;

/**
 * @Author: Ebad Hassan
 * @Date:   2024-09-20 10:50:21
 * @Last Modified by:   Ebad Hassan
 * @Last Modified time: 2024-09-20 10:54:58
 */
namespace InvoiceBulkRegisteration.Logging
{
    /// <summary>
    /// Interface for logging messages. Provides methods for different logging levels.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Logs an informational message on Console Screen.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        void Information(string message);
        /// <summary>
        /// Logs a warning message on Console Screen.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        void Warning(string message);
        /// <summary>
        /// Logs an error message on Console Screen.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="method">The name of the method where the error occurred (optional).</param>
        /// <param name="mailSend">Optional flag indicating if an email notification should be sent (default: false).</param>
        void Error(string message, string method, bool mailSend = default);
        /// <summary>
        /// Logs an unexpected exception of unknown type on Console Screen (or other configured output).
        /// </summary>
        /// <param name="exception">The exception where the exception happend and the stack trace to trace application.</param>
        /// <remarks>
        /// This method is intended for logging unexpected exceptions whose type is unknown. 
        /// It's important to include as much detail as possible in the `message` parameter to aid in debugging.
        /// </remarks>
        void Exception(Exception exception);
        /// <summary>
        /// Logs an welcome message on the screen for current user.
        /// </summary>
        /// <param name="name">The name to be displayed</param>
        void Welcome(string name);
    }
}
