using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.Models
{
    public class EmprestimosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Recebedor")]
        public string Recebedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do Fornecedor")]
        public string Forcenedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do Livro")]
        public string LivroEmprestado { get; set; }

        public DateTime dataUltimaAtualização { get; set; }
    } 
}
