using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Quitta.Models
{
    public class Item
    {
        public string Id { get; set; }
        public TipoItem Tipo { get; set; }
        public string Numero { get; set; }
        public string Fornecedor { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public StatusItem Status { get; set; }

        // Lista de anexos relacionados ao item (boleto/nota)
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        // Propriedade de visualização para a grid: mostra o nome do primeiro anexo ou "Adicionar anexo"
        [JsonIgnore]
        public string AnexoDisplay => (Attachments != null && Attachments.Count > 0) ? Attachments[0].FileName : "Adicionar anexo";
    }

    public enum TipoItem
    {
        Boleto,
        Nota
    }

    public enum StatusItem
    {
        Pendente,
        Pago,
        Vencido
    }
}
