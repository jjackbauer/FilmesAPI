using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class UpdateMovieDTO
    {
        [Required(ErrorMessage = "O campo Título é obrigatório")]
        public string Title { get; set; }
        [Required(ErrorMessage = "O campo Diretor é obrigatório")]
        public string Director { get; set; }
        [StringLength(30, ErrorMessage = "O Gênero deve ter 30 caracteres no máximo")]
        public string Genre { get; set; }
        [Range(1, 600, ErrorMessage = "A Duração deve ter entre 1 e 600 minutos")]
        public int Duration { get; set; }
    }
}
