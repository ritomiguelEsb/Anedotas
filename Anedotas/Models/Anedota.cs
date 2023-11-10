using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Anedotas.Models
{
    public class AnedotaModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contexto { get; set; }
        public string ContextoResposta { get; set; }
        public string Autor { get; set; }


        public AnedotaModel()
        {
            Id = 0;
            Autor = "Anónimo";
            Titulo = "Sem titulo";
            Contexto = string.Empty;
            ContextoResposta = string.Empty;
        }
    }
}
