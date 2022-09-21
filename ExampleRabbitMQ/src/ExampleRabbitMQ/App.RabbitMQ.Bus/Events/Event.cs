﻿namespace App.RabbitMQ.Bus.Events
{
    public abstract class Event 
    {
        public DateTime Timestamp { get; protected set; }

        protected Event() 
        {
            Timestamp = DateTime.Now;
        }
    }
}