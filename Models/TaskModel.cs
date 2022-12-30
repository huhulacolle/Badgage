﻿namespace Badgage.Models
{
    public class TaskModel
    {
        public int? IdTache { get; set; }
        public int IdProjet { get; set; }
        public string NomDeTache { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DateFin { get; set; }
        public DateTime DateCreation { get; set; }
    }
}

