﻿namespace WebApiMoviesUdemy.DTOs
{
    public class PaginacionDTO
    {
        public int pagina { get; set; } = 1;
        private int cantidadRegistrosPorPagina = 10;
        public readonly int CantidadMaximaRegistrosPorPagina = 50;

        public int CantidadRegistrosPorPagina {
            get => cantidadRegistrosPorPagina;
            set
            {
                cantidadRegistrosPorPagina = (value> CantidadMaximaRegistrosPorPagina) ? CantidadMaximaRegistrosPorPagina : value;
            }
        }
    }
}
