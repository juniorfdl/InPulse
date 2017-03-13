namespace Models
{
    using Infra.Base.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class dashboard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public double? PRODUTIVIDADE { get; set; }
        public double? PEDIDOS { get; set; }
        public double? LIGACOES { get; set; }
        public double? CONTATOS { get; set; }
        public double? APROVEITAMENTO { get; set; }
        public DateTime? DATAINICIAL { get; set; }
        public DateTime? DATAFINAL { get; set; }
    }
}
