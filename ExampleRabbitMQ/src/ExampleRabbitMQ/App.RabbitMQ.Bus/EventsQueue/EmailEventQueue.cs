namespace App.RabbitMQ.Bus.EventsQueue
{
    public class EmailEventQueue : Event
    {
        public string Destinatario { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }

        public EmailEventQueue(string destinatario, string titulo, string contenido) 
        {
            Destinatario = destinatario;
            Titulo = titulo;
            Contenido = contenido;
        }
        
    }
}
