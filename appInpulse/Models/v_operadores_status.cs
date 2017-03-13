namespace Models
{
    using Infra.Base.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class v_operadores_status : IEntidadeBase
    {
        [Key]   
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("OPERADOR")]      
        public int id { get; set; }
        public string STATUS_ATUAL { get; set; }
        public Nullable<TimeSpan> TEMPO { get; set; } 
        public string NOME { get; set; }

        [NotMapped]
        public double? PRODUTIVIDADE { get; set; }
        [NotMapped]
        public double? PEDIDOS { get; set; }
        [NotMapped]
        public double? LIGACOES { get; set; }
        [NotMapped]
        public double? CONTATOS { get; set; }
        [NotMapped]
        public double? APROVEITAMENTO { get; set; }
        [NotMapped]
        public string FOTO { get; set; }
        [NotMapped]
        public dynamic VendasPorEstado { get; set; }
        [NotMapped]
        public dynamic MetasXVendas { get; set; }
    }
}
