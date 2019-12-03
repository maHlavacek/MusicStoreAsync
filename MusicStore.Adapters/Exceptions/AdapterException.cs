//@TemplateType:mandatory=ISO:module=Core:import=true
using System;

namespace MusicStore.Adapters.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Stellt Fehler dar, die beim Ausf�hren der Anwendung auftreten.
    /// </summary>
    public partial class AdapterException : Exception
    {
        public int ErrorId { get; } = -1;

        /// <inheritdoc />
        /// <summary>
        /// Initialisiert eine neue Instanz der AdapterException-Klasse 
        /// mit einer angegebenen Fehlermeldung.
        /// </summary>
        /// <param name="identity">Identification der Fehlermeldung.</param>
        /// <param name="message">Die Meldung, in der der Fehler beschrieben wird.</param>
        public AdapterException(int identity, string message)
            : base(message)
        {
            ErrorId = identity;
        }

        public AdapterException(Exception ex)
            : base(ex.Message, ex.InnerException)
        {
        }
    }
}
